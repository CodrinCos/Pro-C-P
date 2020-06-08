using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEscapeSimulator
{
    [Serializable]
    public class FireExtinguisher
    {
        // ---------------------- Fields -----------------------
        private int usesLeft;

        // -------------------- Properties ---------------------
        public int UsesLeft { get { return usesLeft; } }

        // -------------------- Constructors --------------------
        public FireExtinguisher()
        {
            usesLeft = 15;
        }

        // ---------------------- Methods ----------------------

        /// <summary>
        /// Uses the fire extinguisher. Reduces the number of uses left accordingly.
        /// </summary>
        /// <returns>Returns true if there was still at least one use left, returns false if the fire extinguiser was empty.</returns>
        public bool Use()
        {
            if (usesLeft > 0)
            {
                usesLeft--;
                Console.WriteLine("Uses left: " + usesLeft.ToString());
                return true;
            }
            else
                return false;
        }
    }
}
