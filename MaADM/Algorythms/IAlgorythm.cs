using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using MaADM.Common;

namespace MaADM.Algorythms
{
    interface IAlgorythm
    {
        void ClearCores();
        void OnFieldClick(int xInCells, int yInCells, Action<int,int> PlaceCoreElement, Action<int, int> ClearCell);
        void Prepare(List<Element> elements, Action<int> RepaintElements);
        void PerformIteration(List<Element> elements);
        bool CheckFinish(List<Element> elements);
    }
}
