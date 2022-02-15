using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaADM.Algorythms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace MaADM.Common
{
    class FieldController
    {
        const int N_CELLS = 2500;

        List<Element> elementsList;
        PictureBox picture;
        Bitmap bitmap;
        Graphics graphics;
        int fieldWidthInCells, fieldHeightInCells;
        public IAlgorythm Algorythm { get; set; }
        public FieldController(PictureBox picture)
        {
            this.picture = picture;
            Algorythm = new KMiddleAlgorythm();
            (fieldWidthInCells, fieldHeightInCells) = HowToSlice(picture.Size, N_CELLS);
            ClearFieldAndGenerateElements();
        }

        public void ClearFieldAndGenerateElements()
        {
            ClearField(picture.Size);
            Algorythm.ClearCores();
            elementsList = GenerateRandomElements(fieldWidthInCells, fieldHeightInCells, 1000);
        }

        public void OnFieldClick(Point location)
        {
            int xInCells = (int)Math.Floor(location.X / (bitmap.Width / (double)fieldWidthInCells));
            int yInCells = (int)Math.Floor(location.Y / (bitmap.Height / (double)fieldHeightInCells));

            Action<int, int> placeCoreElement = (int x, int y) =>
            {
                using (var g = Graphics.FromImage(bitmap))
                {
                    PlaceElement(x, y, Color.Red, g);
                }
                UpdateBitmap(bitmap);
            };
            Action<int, int> clearCell = (int x, int y) =>
            {
                using (var g = Graphics.FromImage(bitmap))
                {
                    PlaceElement(x, y, Color.White, g);
                }
                UpdateBitmap(bitmap);
            };

            Algorythm.OnFieldClick(xInCells, yInCells, placeCoreElement, clearCell);
        }

        public void Go() //Main proccessing
        {
            Action<int> repaintElements = (int nCategories) =>
            {
                using (var g = Graphics.FromImage(bitmap))
                {
                    foreach (var element in elementsList)
                    {
                        PlaceElement(element.X, element.Y, element.Category / (double)nCategories, g);
                    }
                }
                UpdateBitmap(bitmap);
            };
            Algorythm.Prepare(elementsList, repaintElements);
            Algorythm.PerformIteration(elementsList);
            /*Task task = new Task(() => );
            task.Start();*/
        }

        private void ClearField(Size size)
        {
            bitmap = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
            }
            UpdateBitmap(bitmap);
        }

        private void UpdateBitmap(Bitmap map)
        {
            bitmap = map;
            graphics = Graphics.FromImage(map);
            if (picture != null)
                picture.Image = map;
        }

        private void PlaceElement(int xInCells, int yInCells, double colorBrightnessMultiplyer, Graphics g)
        {
            int colorBrightness = (int)(255 * colorBrightnessMultiplyer);
            PlaceElement(xInCells, yInCells, Color.FromArgb(255, colorBrightness, colorBrightness, colorBrightness), g);
        }

        private void PlaceElement(int xInCells, int yInCells, Color color, Graphics g)
        {
            Brush brush = new SolidBrush(color);

            int width = bitmap.Width / fieldWidthInCells;
            int height = bitmap.Height / fieldHeightInCells;
            int xLeft = xInCells * width;
            int yTop = yInCells * height;
            Rectangle elementRect = new Rectangle(xLeft, yTop, width, height);

            g.FillRectangle(brush, elementRect);
        }

        private (int widthSlices, int heightSlices) HowToSlice(Size size, int nCells)
        {
            int heightSlices = (int)Math.Round(Math.Sqrt(nCells / (size.Width / (double)size.Height)));
            int widthSlices = (int)Math.Round(nCells / (double)heightSlices);
            return (widthSlices, heightSlices);
        }

        private List<Element> GenerateRandomElements(int fieldWidth, int fieldHeight, int nElements)
        {
            Graphics tempGraphics = Graphics.FromImage(bitmap);

            double elementsFrequency = nElements / ((double)fieldWidth * fieldHeight);
            if (elementsFrequency >= 1) throw new ArgumentException("Number of elements must be less than square of the field");

            List<Element> elements = new List<Element>();
            int nElementsPlaced = 0; int nCellsLeft = fieldWidth * fieldHeight;
            Random random = new Random(DateTime.Now.Millisecond);

            for (int i=0; i<fieldHeight; i++)
                for(int j=0; j<fieldWidth; j++)
                {
                    if (nElements - nElementsPlaced >= nCellsLeft ||
                        random.Next(0, 100000) < 100000 * elementsFrequency)
                    {
                        elements.Add(new Element(j, i));
                        PlaceElement(j, i, 0, tempGraphics);
                        nElementsPlaced++;
                        if (nElementsPlaced == nElements) break;
                    }
                    nCellsLeft--;
                }
            UpdateBitmap(bitmap);
            return elements;
        }
    }
}
