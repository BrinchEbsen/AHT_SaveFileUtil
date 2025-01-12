using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHT_SaveFileUtil.Common
{
    public enum Map
    {
        None = -1,
        MapLoading = 0,
        LoadingLoop2D = 1,
        Completely_Swamped = 2,
        Island_Speedway = 3,
        Cavern_Chaos = 4,
        Critter_Calamity = 5,
        All_Washed_Up = 6,
        Cloudy_Speedway = 7,
        Outlandish_Inlet = 8,
        Turtle_Turmoil = 9,
        Snowed_Under = 10,
        Iceberg_Aerobatics = 11,
        Frosty_Flight = 12,
        Iced_TNT = 13,
        Mined_Out = 14,
        Lava_Palaver = 15,
        Sparx_Will_Fly = 16,
        Storming_the_Beach = 17,
        Credits = 18,
        Sunken_Ruins = 19,
        Cloudy_Domain = 20,
        Cloudy_Domain_Ball_Gadget = 21,
        Dragonfly_Falls = 22,
        Crocovile_Swamp = 23,
        Dragon_Village = 24,
        MapR1LinkAB = 25,
        MapR1LinkAC = 26,
        MapExample = 27,
        TestDP = 28,
        MapMorphEnv = 29,
        Dark_Mine = 30,
        Frostbite_Village = 31,
        Gnastys_Cave = 32,
        Ice_Citadel = 33,
        Reds_Lair = 34,
        Gloomy_Glacier = 35,
        Playroom = 36,
        Model_Viewer = 37,
        Test_TL = 38,
        Test_PB = 39,
        Reds_Laboratory = 40,
        Reds_Chamber = 41,
        Test_MF = 42,
        Shop = 43,
        Stormy_Beach = 44,
        Coastal_Remains = 45,
        MapR2LinkAB = 46,
        MapR2LinkAC = 47,
        Test_JS = 48,
        Test_KA = 49,
        Test_NB = 50,
        Test_NB2 = 51,
        Test_SJ = 52,
        Platform_Area = 53,
        TestBeach = 54,
        TestBall = 55,
        Titles = 56,
        MapR4LinkBC = 57,
        MapR4LinkCD = 58,
        MapR4LinkDE = 59,
        Molten_Mount = 60,
        Magma_Falls_Top = 61,
        Magma_Falls_Ball_Gadget = 62,
        Magma_Falls_Bottom = 63,
        Watery_Tomb = 64
    }

    public class MapDataEntry
    {
        public string Name { get; set; } = "N/A";
        public uint FileHash { get; set; } = 0;
        public uint MapHash1 { get; set; } = 0;
        public uint MapHash2 { get; set; } = 0;
        public bool Unused { get; set; } = false;
        public bool DoesPreserve { get; set; } = true;
    }

    public static class MapData
    {
        public static readonly Dictionary<Map, MapDataEntry> MapDataList = new()
        {
            { Map.Completely_Swamped, new()
                {
                    Name = "Completely Swamped",
                    FileHash = 0x010000d3,
                    MapHash1 = 0x05000003,
                    MapHash2 = 0x05000004,
                    DoesPreserve = false
                }
            },
            { Map.Island_Speedway, new()
                {
                    Name = "Island Speedway",
                    FileHash = 0x01000093,
                    MapHash1 = 0x05000003,
                    MapHash2 = 0x05000004,
                    DoesPreserve = false
                }
            },
            { Map.Cavern_Chaos, new()
                {
                    Name = "Cavern Chaos",
                    FileHash = 0x01000094,
                    MapHash1 = 0x05000003,
                    MapHash2 = 0x05000004,
                    DoesPreserve = false
                }
            },
            { Map.Critter_Calamity, new()
                {
                    Name = "Critter Calamity",
                    FileHash = 0x01000092,
                    DoesPreserve = false
                }
            },
            { Map.All_Washed_Up, new()
                {
                    Name = "All Washed Up",
                    FileHash = 0x010000c9,
                    MapHash1 = 0x05000003,
                    MapHash2 = 0x05000004,
                    DoesPreserve = false
                }
            },
            { Map.Cloudy_Speedway, new()
                {
                    Name = "Cloudy Speedway",
                    FileHash = 0x010000d7,
                    MapHash1 = 0x05000003,
                    MapHash2 = 0x05000004,
                    DoesPreserve = false
                }
            },
            { Map.Outlandish_Inlet, new()
                {
                    Name = "Outlandish Inlet",
                    FileHash = 0x010000d6,
                    MapHash1 = 0x05000003,
                    MapHash2 = 0x05000004,
                    DoesPreserve = false
                }
            },
            { Map.Turtle_Turmoil, new()
                {
                    Name = "Turtle Turmoil",
                    FileHash = 0x01000098,
                    MapHash1 = 0x05000003,
                    MapHash2 = 0x05000004,
                    DoesPreserve = false
                }
            },
            { Map.Snowed_Under, new()
                {
                    Name = "Snowed Under",
                    FileHash = 0x010000d4,
                    MapHash1 = 0x05000003,
                    MapHash2 = 0x05000004,
                    DoesPreserve = false
                }
            },
            { Map.Iceberg_Aerobatics, new()
                {
                    Name = "Iceberg Aerobatics",
                    FileHash = 0x0100009a,
                    MapHash1 = 0x05000003,
                    MapHash2 = 0x05000004,
                    DoesPreserve = false
                }
            },
            { Map.Frosty_Flight, new()
                {
                    Name = "Frosty Flight",
                    FileHash = 0x010000c8,
                    MapHash1 = 0x05000003,
                    MapHash2 = 0x05000004,
                    DoesPreserve = false
                }
            },
            { Map.Iced_TNT, new()
                {
                    Name = "Iced TNT",
                    FileHash = 0x010000be,
                    MapHash1 = 0x05000003,
                    MapHash2 = 0x05000004,
                    DoesPreserve = false
                }
            },
            { Map.Mined_Out, new()
                {
                    Name = "Mined Out",
                    FileHash = 0x010000d5,
                    MapHash1 = 0x05000003,
                    MapHash2 = 0x05000004,
                    DoesPreserve = false
                }
            },
            { Map.Lava_Palaver, new()
                {
                    Name = "Lava_Palaver",
                    FileHash = 0x010000a3,
                    MapHash1 = 0x05000003,
                    MapHash2 = 0x05000004,
                    DoesPreserve = false
                }
            },
            { Map.Sparx_Will_Fly, new()
                {
                    Name = "Sparx Will Fly",
                    FileHash = 0x010000b4,
                    MapHash1 = 0x05000003,
                    MapHash2 = 0x05000004,
                    DoesPreserve = false
                }
            },
            { Map.Storming_the_Beach, new()
                {
                    Name = "Storming The Beach",
                    FileHash = 0x010000bf,
                    DoesPreserve = false
                }
            },
            { Map.Credits, new()
                {
                    Name = "Credits",
                    FileHash = 0x0100015c
                }
            },
            { Map.Sunken_Ruins, new()
                {
                    Name = "Sunken Ruins",
                    FileHash = 0x0100005a
                }
            },
            { Map.Cloudy_Domain, new()
                {
                    Name = "Cloudy Domain",
                    FileHash = 0x0100005b,
                    MapHash1 = 0x0500000c
                }
            },
            { Map.Cloudy_Domain_Ball_Gadget, new()
                {
                    Name = "Cloudy Domain (Ball Gadget)",
                    FileHash = 0x0100005b,
                    MapHash1 = 0x0500000b
                }
            },
            { Map.Dragonfly_Falls, new()
                {
                    Name = "Dragonfly Falls",
                    FileHash = 0x0100003a
                }
            },
            { Map.Crocovile_Swamp, new()
                {
                    Name = "Crocovile Swamp",
                    FileHash = 0x01000039
                }
            },
            { Map.Dragon_Village, new()
                {
                    Name = "Dragon Village",
                    FileHash = 0x01000037,
                    MapHash1 = 0x05000009
                }
            },
            { Map.MapR1LinkAB, new()
                {
                    Name = "Tunnel (Dragon Village <-> Crocovile Swamp)",
                    FileHash = 0x0100006d
                }
            },
            { Map.MapR1LinkAC, new()
                {
                    Name = "Ball Gadget (Dragon Village <-> Dragonfly Falls)",
                    FileHash = 0x01000082
                }
            },
            { Map.MapExample, new()
                {
                    Name = "Hogwarts (Map_Example)",
                    FileHash = 0x01000008,
                    MapHash1 = 0x05000002,
                    Unused = true
                }
            },
            { Map.TestDP, new()
                {
                    Name = "Test_DP",
                    FileHash = 0x01000058,
                    Unused = true
                }
            },
            { Map.MapMorphEnv, new()
                {
                    Name = "Test_SC (Map_MorphEnv)",
                    FileHash = 0x01000043,
                    Unused = true
                }
            },
            { Map.Dark_Mine, new()
                {
                    Name = "Dark Mine",
                    FileHash = 0x010000a1
                }
            },
            { Map.Frostbite_Village, new()
                {
                    Name = "Frostbite Village",
                    FileHash = 0x01000038,
                    MapHash1 = 0x05000009
                }
            },
            { Map.Gnastys_Cave, new()
                {
                    Name = "Gnasty's Cave",
                    FileHash = 0x01000087
                }
            },
            { Map.Ice_Citadel, new()
                {
                    Name = "Ice Citadel",
                    FileHash = 0x01000059
                }
            },
            { Map.Reds_Lair, new()
                {
                    Name = "Red's Lair",
                    FileHash = 0x010000ed
                }
            },
            { Map.Gloomy_Glacier, new()
                {
                    Name = "Gloomy Glacier",
                    FileHash = 0x0100005c
                }
            },
            { Map.Playroom, new()
                {
                    Name = "PlayRoom",
                    FileHash = 0x0100000e,
                    Unused = true
                }
            },
            { Map.Model_Viewer, new()
                {
                    Name = "ModelViewer",
                    FileHash = 0x01000074,
                    Unused = true
                }
            },
            { Map.Test_TL, new()
                {
                    Name = "Test_TL",
                    FileHash = 0x0100001d,
                    Unused = true
                }
            },
            { Map.Test_PB, new()
                {
                    Name = "Test_PB",
                    FileHash = 0x0100001b,
                    Unused = true
                }
            },
            { Map.Reds_Laboratory, new()
                {
                    Name = "Red's Laboratory",
                    FileHash = 0x010000a2
                }
            },
            { Map.Reds_Chamber, new()
                {
                    Name = "Red's Chamber",
                    FileHash = 0x010000e5
                }
            },
            { Map.Test_MF, new()
                {
                    Name = "Test_MF (Rock Golem)",
                    FileHash = 0x0100001a,
                    Unused = true
                }
            },
            { Map.Shop, new()
                {
                    Name = "Shop",
                    FileHash = 0x0100003d,
                    Unused = true
                }
            },
            { Map.Stormy_Beach, new()
                {
                    Name = "Stormy Beach",
                    FileHash = 0x0100005d
                }
            },
            { Map.Coastal_Remains, new()
                {
                    Name = "Coastal Remains",
                    FileHash = 0x0100003b
                }
            },
            { Map.MapR2LinkAB, new()
                {
                    Name = "Elevator (Coastal Remains <-> Sunken Ruins)",
                    FileHash = 0x01000150
                }
            },
            { Map.MapR2LinkAC, new()
                {
                    Name = "Elevator (Coastal Remains <-> Cloudy Domain)",
                    FileHash = 0x01000151
                }
            },
            { Map.Test_JS, new()
                {
                    Name = "Test_JS",
                    FileHash = 0x0100001e,
                    Unused = true
                }
            },
            { Map.Test_KA, new()
                {
                    Name = "Test_KA",
                    FileHash = 0x01000009,
                    Unused = true
                }
            },
            { Map.Test_NB, new()
                {
                    Name = "Test_NB",
                    FileHash = 0x01000007,
                    Unused = true
                }
            },
            { Map.Test_NB2, new()
                {
                    Name = "Test_NB2",
                    FileHash = 0x0100015d,
                    Unused = true
                }
            },
            { Map.Test_SJ, new()
                {
                    Name = "Test_SJ",
                    FileHash = 0x0100000a,
                    Unused = true
                }
            },
            { Map.Platform_Area, new()
                {
                    Name = "Test_WTS (Platform_Area)",
                    FileHash = 0x01000020,
                    Unused = true
                }
            },
            { Map.TestBeach, new()
                {
                    Name = "TestBeach",
                    FileHash = 0x0100002b,
                    Unused = true
                }
            },
            { Map.Titles, new()
                {
                    Name = "Title Screen",
                    FileHash = 0x01000029,
                    MapHash1 = 0x05000005,
                    DoesPreserve = false
                }
            },
            { Map.MapR4LinkBC, new()
                {
                    Name = "Tunnel (Molten Mount <-> Magma Falls)",
                    FileHash = 0x01000154
                }
            },
            { Map.MapR4LinkCD, new()
                {
                    Name = "Tunnel (Magma Falls <-> Dark Mine)",
                    FileHash = 0x01000156
                }
            },
            { Map.MapR4LinkDE, new()
                {
                    Name = "Tunnel (Dark Mine <-> Red's Laboratory)",
                    FileHash = 0x01000155
                }
            },
            { Map.Molten_Mount, new()
                {
                    Name = "Molten Mount",
                    FileHash = 0x0100005E
                }
            },
            { Map.Magma_Falls_Top, new()
                {
                    Name = "Magma Falls Top",
                    FileHash = 0x0100005F,
                    MapHash1 = 0x0500000b
                }
            },
            { Map.Magma_Falls_Ball_Gadget, new()
                {
                    Name = "Magma Falls (Ball Gadget)",
                    FileHash = 0x0100005F,
                    MapHash1 = 0x0500000c
                }
            },
            { Map.Magma_Falls_Bottom, new()
                {
                    Name = "Magma Falls Bottom",
                    FileHash = 0x0100005F,
                    MapHash1 = 0x0500000d
                }
            },
            { Map.Watery_Tomb, new()
                {
                    Name = "Watery Tomb",
                    FileHash = 0x010000EC
                }
            }
        };
    }
}
