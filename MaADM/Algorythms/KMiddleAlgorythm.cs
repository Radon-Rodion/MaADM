using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using MaADM.Common;

namespace MaADM.Algorythms
{
    class KMiddleAlgorythm : IAlgorythm
    {
        private List<Element> coreElementsList;
        private Element[] coreElementsArray;
        private Action<int> RepaintElements;
        private Action<int, int> PlaceCoreElement;
        private Action<int, int> ClearCell;

        public KMiddleAlgorythm()
        {
            coreElementsList = new List<Element>();
        }
        public void ClearCores()
        {
            coreElementsList.Clear();
        }
        public bool CheckFinish(List<Element> elements)
        {
            var newCoresArray = DefineNewCores(elements);
            foreach (var oldCore in coreElementsArray)
            {
                ClearCell(oldCore.X, oldCore.Y);
            }
            foreach (var core in newCoresArray)
            {
                PlaceCoreElement(core.X, core.Y);
            }
            //Thread.Sleep(1000);
            for(int i=0; i<newCoresArray.Length; i++)
            {
                if (newCoresArray[i].X != coreElementsArray[i].X || newCoresArray[i].Y != coreElementsArray[i].Y)
                {
                    coreElementsArray = newCoresArray;
                    return false;
                }
            }
            return true;
        }

        public void OnFieldClick(int xInCells, int yInCells, Action<int, int> PlaceCoreElement, Action<int, int> ClearCell)
        {
            this.PlaceCoreElement = PlaceCoreElement;
            this.ClearCell = ClearCell;
            coreElementsList.Add(new Element(xInCells, yInCells));
            PlaceCoreElement(xInCells, yInCells);
        }

        public void PerformIteration(List<Element> elements)
        {
            while (!CheckFinish(elements))
            {
                RegroupToNearest(elements);
                RepaintElements(coreElementsArray.Length);
            }
        }

        public void Prepare(List<Element> elements, Action<int> RepaintElements)
        {
            this.RepaintElements = RepaintElements;
            coreElementsArray = coreElementsList.ToArray();
            RegroupToNearest(elements);
            RepaintElements(coreElementsArray.Length);
        }

        private void RegroupToNearest(List<Element> elements)
        {
            foreach(var element in elements)
            {
                int closestIndex = 0;
                for(int i=1; i<coreElementsArray.Length; i++)
                {
                    if (element.DistanceTo(coreElementsArray[closestIndex]) > element.DistanceTo(coreElementsArray[i]))
                        closestIndex = i;
                }
                element.Category = closestIndex;
            }
        }

        private Element[] DefineNewCores(List<Element> elements)
        {
            long[] sumXs = new long[coreElementsArray.Length];
            long[] sumYs = new long[coreElementsArray.Length];
            int[] amounts = new int[coreElementsArray.Length];
            foreach(var element in elements)
            {
                sumXs[element.Category] += element.X;
                sumYs[element.Category] += element.Y;
                amounts[element.Category]++;
            }
            Element[] newCoresArray = new Element[coreElementsArray.Length];
            for(int i=0; i<coreElementsArray.Length; i++)
            {
                newCoresArray[i] = new Element((int)(sumXs[i] / amounts[i]), (int)(sumYs[i] / amounts[i]));
            }
            return newCoresArray;
        }
    }
}
