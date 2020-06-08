using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FireEscapeSimulator
{
    class FileHelper
    {
        public FileHelper(){}

        /// <summary>
        /// Saves the state of the simulation
        /// </summary>
        public void SaveSimulation(Simulation s, string x)
        {         
            //serialize the list and the matrix 
            using (Stream stream = File.Open(x + ".fes", FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, s);
            }
        }
        /// <summary>
        /// Loads simulation from a file
        /// </summary>
        /// <param name="window"></param>
        /// <returns>a simulation object</returns>
        public Simulation LoadSimulation(h window)
        {
            //Open a file dialog, set i
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Simulation files|*.fes*";
            fileDialog.FilterIndex = 1;
            fileDialog.Multiselect = false;
            string path = "";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                path = fileDialog.FileName;
                Simulation deserializedSimulation = new Simulation(window.loadedMap);
                using (Stream stream = File.Open(path, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    deserializedSimulation = (Simulation)bin.Deserialize(stream);
                }
                return deserializedSimulation;
            }
            else
            {
                return null;
            }
        }
    }
}
