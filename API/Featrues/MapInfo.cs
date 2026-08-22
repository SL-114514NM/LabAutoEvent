using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMER;
using ProjectMER.Features;
using ProjectMER.Features.Objects;
using UnityEngine;

namespace LabAutoEvent.API.Featrues
{
    public class MapInfo
    {
        public MapInfo() { }
        public MapInfo(string mapname, Vector3 pos)
        {
            this.Position = pos;
            this.MapObject = ObjectSpawner.SpawnSchematic(mapname, pos);
        }
        public MapInfo(SchematicObject mapObject, Vector3 position)
        {
            MapObject = mapObject;
            Position = position;
        }

        public SchematicObject MapObject { get; set; }
        public Vector3 Position { get; set; }
    }
}
