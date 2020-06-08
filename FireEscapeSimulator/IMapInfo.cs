using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEscapeSimulator
{
    interface IMapInfo
    {
        List<Tile> GetRoom(Point location);
        List<Person> GetStudentsInRoom(Point location);
        Tile GetTile(Point location);
        bool OpenOrCloseWindow(Point location);
        int GetFloorNumber();
    }
}
