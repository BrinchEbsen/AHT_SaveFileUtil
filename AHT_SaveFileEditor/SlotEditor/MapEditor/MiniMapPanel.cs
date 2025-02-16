using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save.MiniMap;
using AHT_SaveFileUtil.Save.Slot;
using AHT_SaveFileUtil.Save.Triggers;
using System.ComponentModel;

namespace AHT_SaveFileEditor.SlotEditor.MapEditor
{
    public enum PaintMode
    {
        None = 0,
        Reveal = 1,
        Unreveal = 2
    }

    internal class MiniMapPanel : Panel
    {
        private const int TEXTURE_SIZE = 512;

        private readonly Font TextFont = new Font(FontFamily.GenericSansSerif, 20);
        private readonly Brush TextBrush = new SolidBrush(Color.White);

        private readonly GameState _gameState;
        private readonly Map _mapIndex;
        private MiniMapInfo? _mappedInfo;
        private MiniMapInfo? _backgroundInfo;

        private Image? _miniMapTexture = null;
        private Image? _revealBlob = null;

        private readonly Pen gridViewPen = new(Color.Red, 2);
        private readonly Pen triggerPen = new(Color.Blue, 5);
        private readonly Pen startPointPen = new(Color.Green, 8);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PaintMode PaintMode { get; set; } = PaintMode.None;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool UsingDefault { get; set; } = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowMiniMap { get; set; } = true;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowSquares { get; set; } = true;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public EXVector3? TriggerHighLight { get; private set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public EXVector3? StartPointHighLight { get; private set; }

        public MiniMapPanel(GameState gameState, Map mapIndex)
        {
            _gameState = gameState;
            _mapIndex = mapIndex;

            Size = new Size(TEXTURE_SIZE, TEXTURE_SIZE);
            DoubleBuffered = true;

            Paint += MiniMapPanel_Paint;

            MouseClick += MiniMapPanel_MouseClick;
            MouseMove += MiniMapPanel_MouseMove;

            var rsc = ResourceHandler.Instance;
            if (rsc.AllYamlLoaded && rsc.AllTexturesLoaded)
            {
                if (!SetUpTextures())
                    Controls.Add(new Label() { Text = "Could not set up textures." });
            }
            else
            {
                Controls.Add(new Label() { Text = "Required YAML/Texture files could not be found." });
            }
        }

        private void MiniMapPanel_MouseMove(object? sender, MouseEventArgs e)
        {
            DoMouseEvent(e);
        }

        private void MiniMapPanel_MouseClick(object? sender, MouseEventArgs e)
        {
            DoMouseEvent(e);
        }

        /// <summary>
        /// Called when the mouse should draw on the minimap.
        /// </summary>
        private void DoMouseEvent(MouseEventArgs e)
        {
            //If no map reveal blobs, no need to process mouse events
            if (UsingDefault) return;

            if (e.X < 0 || e.X >= TEXTURE_SIZE) return;
            if (e.Y < 0 || e.Y >= TEXTURE_SIZE) return;

            if (e.Button == MouseButtons.Left)
            {
                TryDrawReveal(e);
                Invalidate();
            }
        }

        private void TryDrawReveal(MouseEventArgs e)
        {
            if (PaintMode == PaintMode.None) return;
            if (_mappedInfo == null) return;
            if (_backgroundInfo == null) return;
            if (ResourceHandler.Instance.MiniMaps == null) return;

            float[] worldPos = _backgroundInfo.GetWorldPositionFromPixelCoords(e.X, TEXTURE_SIZE - e.Y, TEXTURE_SIZE);

            if (worldPos[0] < _mappedInfo.WorldEdge[0] ||
                worldPos[0] > _mappedInfo.WorldEdge[2] + _mappedInfo.PixelEdge[2] * 0.5f ||
                worldPos[1] < _mappedInfo.WorldEdge[3] ||
                worldPos[1] > _mappedInfo.WorldEdge[1] + _mappedInfo.PixelEdge[3] * 0.5f)
                return;

            int offset = _mappedInfo.GetBitHeapOffset(
                worldPos[0],
                worldPos[1]);

            int size = _mappedInfo.BitHeapSize;

            if (offset > size) return;

            int address = ResourceHandler.Instance.MiniMaps.GetMiniMapInfoBitHeapOffset(_mappedInfo);
            if (address < 0) return;

            byte value = PaintMode == PaintMode.Reveal ? (byte)1 : (byte)0;

            if (_gameState.BitHeap.ValidAllocatedBitHeapAddress(address + offset))
                _gameState.BitHeap.WriteBits(1, [value], address + offset);
        }

        private bool SetUpTextures()
        {
            //PRE: the "MiniMaps" resource is loaded

            var rsc = ResourceHandler.Instance;

            if (!MapData.MapDataList.TryGetValue(_mapIndex, out MapDataEntry? entry))
                return false;

            uint fileHash = entry.FileHash;
            uint mapHash = entry.MapHash1;

            //Get the minimap info for this level.
            if (entry.MiniMapDistinguishedByMapHash)
            {
                _backgroundInfo = rsc.MiniMaps!.GetInfo(fileHash, mapHash, InfoType.Background);
                _mappedInfo = rsc.MiniMaps!.GetInfo(fileHash, mapHash, InfoType.Mapped);
            } else
            {
                _backgroundInfo = rsc.MiniMaps!.GetInfo(fileHash, InfoType.Background);
                _mappedInfo = rsc.MiniMaps!.GetInfo(fileHash, InfoType.Mapped);
            }

            if (_backgroundInfo == null || _mappedInfo == null)
            {
                UsingDefault = true;
                return SetUpDefaultTexture(rsc);
            }

            UsingDefault = false;

            return SetUpMapTexture(rsc);
        }

        private bool SetUpDefaultTexture(ResourceHandler rsc)
        {
            if (rsc.MapTextures == null) return false;

            if (!rsc.MapTextures.TryGetValue(EXHashCode.HT_Texture_NoMap, out Image? img))
                return false;

            _miniMapTexture = img;

            return true;
        }

        private bool SetUpMapTexture(ResourceHandler rsc)
        {
            //Guards:

            if (rsc.MapTextures == null) return false;
            if (rsc.MiniMaps == null) return false;
            if (_backgroundInfo == null) return false;

            if (!Enum.IsDefined(typeof(EXHashCode), _backgroundInfo.Texture))
                return false;

            //Find textures

            if (!rsc.MapTextures.TryGetValue((EXHashCode)_backgroundInfo.Texture, out Image? mapImg))
                return false;

            _miniMapTexture = mapImg;

            if (!rsc.MapTextures.TryGetValue(EXHashCode.HT_Texture_MAP_MiniMapIcon_FillBlob, out Image? blobImg))
                return false;

            _revealBlob = blobImg;

            return true;
        }

        private void MiniMapPanel_Paint(object? sender, PaintEventArgs e)
        {
            //Check that textures are loaded properly
            if (_miniMapTexture == null) return;
            if (!UsingDefault && _revealBlob == null) return;

            var graphics = e.Graphics;
            graphics.Clear(Color.Black);

            if (!UsingDefault)
                DrawRevealedArea(graphics);

            if (ShowMiniMap)
                graphics.DrawImage(_miniMapTexture, 0, 0);

            if (UsingDefault)
                graphics.DrawString("No Minimap", TextFont, TextBrush, 10, 10);

            if (!UsingDefault)
                DrawHighLight(graphics);

            /*
            graphics.DrawString(testx.ToString(),
                new Font(FontFamily.GenericSansSerif, 18),
                new SolidBrush(Color.Red),
                20, 20
                );

            graphics.DrawString(testy.ToString(),
                new Font(FontFamily.GenericSansSerif, 18),
                new SolidBrush(Color.Red),
                20, 50
                );

            graphics.DrawString(testoffset.ToString(),
                new Font(FontFamily.GenericSansSerif, 18),
                new SolidBrush(Color.Red),
                20, 80
                );

            graphics.DrawString(testwx.ToString(),
                new Font(FontFamily.GenericSansSerif, 18),
                new SolidBrush(Color.Red),
                20, 110
                );

            graphics.DrawString(testwz.ToString(),
                new Font(FontFamily.GenericSansSerif, 18),
                new SolidBrush(Color.Red),
                20, 140
                );
            */
        }

        /// <summary>
        /// Draw the "revealed" area from the minimap data.
        /// </summary>
        private void DrawRevealedArea(Graphics graphics)
        {
            if (UsingDefault || _revealBlob == null) return; 
            if (_mappedInfo == null || _backgroundInfo == null) return;
            
            var miniMaps = ResourceHandler.Instance.MiniMaps;
            if (miniMaps == null) return;
            
            int address = miniMaps.GetMiniMapInfoBitHeapOffset(_mappedInfo);
            if (address < 0) return;

            //Get dimensions
            int[] dim = _mappedInfo.MappedGridSize;

            //Get bits
            var span = new BitSpanReader(
                _gameState.BitHeap.ReadBits(_mappedInfo.BitHeapSize, address));

            //Iterate through all cells and draw them
            for (int z = 0; z < dim[1]; z++)
            {
                for (int x = 0; x < dim[0]; x++)
                {
                    //Only draw if this cell's bit is set
                    if (span.NextBit != 0)
                    {
                        //Get the cell world position
                        float[] cellPos = GetCellWorldPosition(x, z);

                        //Get the pixel coordinates of the cell
                        int[] pixPos = _backgroundInfo.GetPixelCoordsFromWorldPosition(
                            cellPos[0], cellPos[1], TEXTURE_SIZE);

                        //Draw debug squares to easily see the grid
                        if (ShowSquares)
                            graphics.DrawRectangle(gridViewPen,
                            new Rectangle(
                                pixPos[0] - 5,
                                pixPos[1] - 5,
                                10,
                                10));
                        //Draw the blob texture accurate to the game
                        else
                            graphics.DrawImage(_revealBlob,
                            pixPos[0] - _mappedInfo.PixelEdge[0],
                            pixPos[1] - _mappedInfo.PixelEdge[0]);
                    }
                }
            }
        }

        private void DrawHighLight(Graphics graphics)
        {
            if (_backgroundInfo == null) return;

            if (StartPointHighLight != null)
            {
                int[] pixCoords = _backgroundInfo.GetPixelCoordsFromWorldPosition(
                StartPointHighLight!.X, StartPointHighLight!.Z, TEXTURE_SIZE);

                if (CoordsWithinTextureArea(pixCoords[0], pixCoords[1]))
                    graphics.DrawEllipse(startPointPen, pixCoords[0] - 5, pixCoords[1] - 5, 10, 10);
            }
            
            if (TriggerHighLight != null)
            {
                int[] pixCoords = _backgroundInfo.GetPixelCoordsFromWorldPosition(
                TriggerHighLight!.X, TriggerHighLight!.Z, TEXTURE_SIZE);

                if (CoordsWithinTextureArea(pixCoords[0], pixCoords[1]))
                    graphics.DrawEllipse(triggerPen, pixCoords[0] - 6, pixCoords[1] - 6, 12, 12);
            }
        }

        private bool CoordsWithinTextureArea(int x, int y)
        {
            return (x >= 0 && x < TEXTURE_SIZE &&
                    y >= 0 && y < TEXTURE_SIZE);
        }

        /// <summary>
        /// Convert a cell coordinate into a world coordinate.
        /// </summary>
        private float[] GetCellWorldPosition(int x, int z)
        {
            if (_mappedInfo == null) return [0, 0];

            return [
                _mappedInfo.WorldEdge[0] + (x * _mappedInfo.PixelEdge[2]) + (_mappedInfo.PixelEdge[2] * 0.5f),
                _mappedInfo.WorldEdge[3] + (z * _mappedInfo.PixelEdge[3]) + (_mappedInfo.PixelEdge[3] * 0.5f)
                ];
        }

        private int[] GetCellArrayPosition(float x, float z)
        {
            if (_mappedInfo == null) return [0, 0];

            if (_mappedInfo.PixelEdge[2] == 0 || _mappedInfo.PixelEdge[3] == 0)
                return [0, 0];

            return [
                (int)((x - _mappedInfo.WorldEdge[0] - (_mappedInfo.PixelEdge[2] * 0.5f)) / _mappedInfo.PixelEdge[2]),
                (int)((z - _mappedInfo.WorldEdge[3] - (_mappedInfo.PixelEdge[3] * 0.5f)) / _mappedInfo.PixelEdge[3])
                ];
        }

        public void ClearMiniMap()
        {
            if (_mappedInfo == null) return;
            if (ResourceHandler.Instance.MiniMaps == null) return;

            int address = ResourceHandler.Instance.MiniMaps.GetMiniMapInfoBitHeapOffset(_mappedInfo);
            if (address < 0) return;

            _gameState.BitHeap.ClearBits(address, _mappedInfo.BitHeapSize);
        }

        public void FillMiniMap()
        {
            if (_mappedInfo == null) return;
            if (ResourceHandler.Instance.MiniMaps == null) return;

            int address = ResourceHandler.Instance.MiniMaps.GetMiniMapInfoBitHeapOffset(_mappedInfo);
            if (address < 0) return;

            _gameState.BitHeap.SetBits(address, _mappedInfo.BitHeapSize);
        }

        public void SetTriggerHighLight(EXVector3 position)
        {
            TriggerHighLight = new EXVector3(position);
        }

        public void ClearTriggerHighLight()
        {
            TriggerHighLight = null;
        }

        public void SetStartPointHighLight(uint startPoint, GeoMap mapData)
        {
            int index = mapData.GetStartPointTriggerIndex(startPoint);
            if (index < 0) return;

            SetStartPointHighLight(mapData.TriggerList[index].Position);
        }

        public void SetStartPointHighLight(int trigIndex, GeoMap mapData)
        {
            SetStartPointHighLight(mapData.TriggerList[trigIndex].Position);
        }

        public void SetStartPointHighLight(EXVector3 position)
        {
            StartPointHighLight = new EXVector3(position);
        }

        public void ClearStartPointHighLight()
        {
            StartPointHighLight = null;
        }
    }
}
