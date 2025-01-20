using AHT_SaveFileUtil.Common;
using AHT_SaveFileUtil.Save.MiniMap;
using AHT_SaveFileUtil.Save.Slot;
using System.ComponentModel;

namespace AHT_SaveFileEditor.SlotEditor.MapEditor
{
    internal class MiniMapPanel : Panel
    {
        private GameState _gameState;
        private Map _mapIndex;
        private MiniMapInfo? _mappedInfo;
        private MiniMapInfo? _backgroundInfo;

        private Image? _miniMapTexture = null;
        private Image? _revealBlob = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool UsingDefault { get; set; } = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowMiniMap { get; set; } = true;

        public MiniMapPanel(GameState gameState, Map mapIndex)
        {
            this._gameState = gameState;
            this._mapIndex = mapIndex;

            Size = new Size(512, 512);
            DoubleBuffered = true;

            Paint += MiniMapPanel_Paint;

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
            {
                DrawRevealedArea(graphics);
            }

            if (ShowMiniMap)
                graphics.DrawImage(_miniMapTexture, 0, 0);
        }

        private void DrawRevealedArea(Graphics graphics)
        {
            if (UsingDefault || _revealBlob == null) return; 
            if (_mappedInfo == null || _backgroundInfo == null) return;
            
            var miniMaps = ResourceHandler.Instance.MiniMaps;
            if (miniMaps == null) return;
            
            int address = miniMaps.GetMiniMapInfoOffset(_mappedInfo);
            if (address < 0) return;

            bool[][] array = _mappedInfo.GetArrayFromBitHeap(_gameState.BitHeap, address);

            float[] bottomLeft = [
                    _mappedInfo.WorldEdge[0],
                    _mappedInfo.WorldEdge[3]
                ];

            for (int z = 0; z < array.Length; z++)
            {
                for (int x = 0; x < array[z].Length; x++)
                {
                    float cellPosX = bottomLeft[0] + x * _mappedInfo.PixelEdge[2] + (_mappedInfo.PixelEdge[2] * 0.5f);
                    float cellPosZ = bottomLeft[1] + z * _mappedInfo.PixelEdge[3] + (_mappedInfo.PixelEdge[3] * 0.5f);

                    int[] pixPos = _backgroundInfo.GetPixelCoordsFromWorldPosition(
                        cellPosX, cellPosZ, 512);

                    if (array[z][x])
                    {
                        graphics.DrawImage(_revealBlob,
                            pixPos[0] - _mappedInfo.PixelEdge[0],
                            pixPos[1] - _mappedInfo.PixelEdge[0]);
                    }
                }
            }
        }

        private bool GetMappedAreaPointAndResolution(out PointF topLeft, out float resX, out float resY, int lenX, int lenY)
        {
            bool success = _backgroundInfo!.GetTextureWorldEdges(
                out float xLeft, out float xRight, out float zUp, out float zBottom);

            if (!success)
            {
                topLeft = new Point(0, 0);
                resX = 0; resY = 0;
                return false;
            }

            topLeft = new PointF(
                0f,
                0f
                );

            float xSpan = _mappedInfo.WorldEdge[2] - _mappedInfo.WorldEdge[0];
            float ySpan = _mappedInfo.WorldEdge[1] - _mappedInfo.WorldEdge[3];

            resX = xSpan / lenX;
            resY = ySpan / lenY;

            return true;
        }
    }
}
