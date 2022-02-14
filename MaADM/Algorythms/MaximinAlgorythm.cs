using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaADM.Common;

namespace MaADM.Algorythms
{
    class MaximinAlgorythm : IAlgorythm
    {
        private List<Element> coreElementsList;
        private Element[] coreElementsArray;
        private Action<int> RepaintElements;
        private Action<int, int> PlaceCoreElement;
        private Action<int, int> ClearCell;

        public MaximinAlgorythm()
        {
            coreElementsList = new List<Element>();
        }

        public void ClearCores()
        {
            throw new NotImplementedException();
        }
        public bool CheckFinish(List<Element> elements)
        {
            throw new NotImplementedException();
        }

        public void OnFieldClick(int xInCells, int yInCells, Action<int, int> PlaceCoreElement, Action<int, int> ClearCell)
        {
            throw new NotImplementedException();
        }

        public void PerformIteration(List<Element> elements)
        {
            throw new NotImplementedException();
        }

        public void Prepare(List<Element> elements, Action<int> RepaintElements)
        {
            throw new NotImplementedException();
        }
    }
}
