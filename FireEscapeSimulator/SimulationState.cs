using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FireEscapeSimulator
{

    /// <summary>
    /// This class represents the state of a simulation at the time an instance of this class was created. It can be restored at a later time.
    /// </summary>
    class SimulationState
    {
        // ---------------------- Fields -----------------------
        private byte[] state;

        // -------------------- Properties ---------------------
        public byte[] State { get { return state; } }

        // -------------------- Constructors --------------------
        public SimulationState(Simulation simulation)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter bin = new BinaryFormatter();
            bin.Serialize(stream, simulation);
            state  = stream.GetBuffer();
        }
    }
}
