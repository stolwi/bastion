using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bastion.Entity.Map
{
    class PrecisePosition3
    {
        private DoublePosition3 precisePosition;  //double precision for fractional movements
        private Position3 position;               // integer value, should reflect precisePosition rounded
        private Position3 mapSpotPosition;        // integer value, in tile references

        public PrecisePosition3()
        {
            ExactPos = new DoublePosition3(0,0,0);
        }

        public DoublePosition3 ExactPos
        { 
            get 
            {
                return precisePosition;
            }
            set
            {
                precisePosition = value;
                ClearCaches();
            }

        }

        public Position3 Pos 
        {
            get 
            {
                // Cache the rounded value.
                return position ?? (position = precisePosition);
            }
            set 
            {
                precisePosition = value;
                ClearCaches();
            }
        }

        public Position3 TilePos
        {
            get
            {
                // Cache the computed value.
                return mapSpotPosition ?? (mapSpotPosition = position.DivideBy(GameConfig.Instance.TileSize));
            }
        }

        public void ExactMove(double x, double y, double z)
        {
            ExactPos.Move(x, y, z);
            ClearCaches();
        }

        private void ClearCaches()
        {
            position = null;
            mapSpotPosition = null;
        }
    }
}
