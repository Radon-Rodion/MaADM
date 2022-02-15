using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
            coreElementsList.Clear();
        }
        public bool CheckFinish(List<Element> elements)
        {
            var newCore = DefineNewCore(elements);
            //Thread.Sleep(1000);
            if (newCore == null) return true;
            coreElementsList.Add(newCore);
            coreElementsArray = coreElementsList.ToArray();
            PlaceCoreElement(newCore.X, newCore.Y);
            return false;
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
            IAlgorythm kMiddle = new KMiddleAlgorythm();
            foreach (var core in coreElementsArray)
            {
                kMiddle.OnFieldClick(core.X, core.Y, PlaceCoreElement, ClearCell);
            }
            kMiddle.Prepare(elements, RepaintElements);
            kMiddle.PerformIteration(elements);
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
            foreach (var element in elements)
            {
                int closestIndex = 0;
                for (int i = 1; i < coreElementsArray.Length; i++)
                {
                    if (element.DistanceTo(coreElementsArray[closestIndex]) > element.DistanceTo(coreElementsArray[i]))
                        closestIndex = i;
                }
                element.Category = closestIndex;
            }
        }

        private Element DefineNewCore(List<Element> elements)
        {
            Element[] elementsArray = elements.ToArray();
            int[] farestIndexesInCategories = new int[coreElementsArray.Length];
            for(int i=0; i<elementsArray.Length; i++)
            {
                if(elementsArray[i].DistanceTo(coreElementsArray[elementsArray[i].Category]) >
                    elementsArray[farestIndexesInCategories[elementsArray[i].Category]].DistanceTo(coreElementsArray[elementsArray[i].Category]))
                {
                    farestIndexesInCategories[elementsArray[i].Category] = i;
                }
            }

            int farestIndex = 0;
            for(int j=1; j<farestIndexesInCategories.Length; j++)
            {
                if(elementsArray[farestIndexesInCategories[j]].DistanceTo(coreElementsArray[elementsArray[farestIndexesInCategories[j]].Category]) >
                    elementsArray[farestIndexesInCategories[farestIndex]].DistanceTo(coreElementsArray[elementsArray[farestIndexesInCategories[farestIndex]].Category]))
                {
                    farestIndex = j;
                }
            }

            if (GetAverageDistanceBetweenCores() > 2 * elementsArray[farestIndexesInCategories[farestIndex]].DistanceTo(coreElementsArray[elementsArray[farestIndexesInCategories[farestIndex]].Category]))
                return null;
            return elementsArray[farestIndexesInCategories[farestIndex]];
        }

        private double GetAverageDistanceBetweenCores()
        {
            double distance = 0;
            foreach(var core in coreElementsArray)
            {
                foreach(var toCore in coreElementsArray)
                {
                    distance += core.DistanceTo(toCore);
                }
            }
            return distance / (coreElementsArray.Length * 2);
        }
    }
}
