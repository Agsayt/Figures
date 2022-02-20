using Figures.Logic;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Figures
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            FigureCB.Items.AddRange(Enum.GetNames(typeof(Figures)));
            FigureCB.SelectedIndex = 0;
        }

        enum Figures
        {
            Circle,
            Square,
            Triangle
        }

        #region vars
        List<Figure> figureList = new List<Figure>();
        Figure selectedFigure;
        Figures figToCreate;
        #endregion


        private void Form1_Resize(object sender, EventArgs e)
        {
            canvas.Width = this.Width - 10;
            canvas.Height = this.Height - 10;
        }

        private void FigureCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (sender as ToolStripComboBox);

            if (item == null)
                return;

            switch(item.SelectedItem.ToString())
            {
                case "Circle":
                    figToCreate = Figures.Circle;
                    break;
                case "Square":
                    figToCreate= Figures.Square;
                    break;
                case "Triangle":
                    figToCreate = Figures.Triangle;
                    break;
            }

            canvas.Focus();
        }

        private void AddFigure_Click(object sender, EventArgs e)
        {
            var middlePoint = new PointF(canvas.Width / 2, canvas.Height / 2);
            int id = 0;

            switch(figToCreate)
            {
                case Figures.Circle:
                    var circle = new Circle(middlePoint.X, middlePoint.Y);
                    circle.SetRadius(20); 
                    id = figureList.Where(x => x.name.Contains("circle")).Count() + 1;
                    circle.localId = id;
                    circle.name = "circle" + id;
                    figureList.Add(circle);
                    FiguresLB.Items.Add(circle);
                    break;
                case Figures.Square:
                    var square = new Square(100, middlePoint.X, middlePoint.Y);                    
                    id = figureList.Where(x => x.name.Contains("square")).Count() + 1;
                    square.localId = id;
                    square.name = "square" + id;
                    figureList.Add(square);
                    FiguresLB.Items.Add(square);
                    break;
                case Figures.Triangle: 
                    
                    break;
            }

            canvas.Invalidate();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            foreach (var item in figureList)
            {
                item.Draw(e.Graphics);
            }

            
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            selectedFigure = null;
            foreach (var figure in figureList)
            {
                figure.isSelected = false;
            }

            foreach (var figure in figureList)
            {                
                if (figure.IsInside(e.X, e.Y))
                {
                    figure.isSelected = true;
                    selectedFigure = figure;
                    break;
                }
            }
            canvas.Invalidate(true);
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                foreach (var figure in figureList)
                {
                    if (figure.isSelected)
                    {
                        figure.posX = e.X;
                        figure.posY = e.Y;
                    }
                }
                canvas.Invalidate();
            }         

        }

        private void FiguresLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var fgr in figureList)
            {
                fgr.isSelected = false;
            }

            var item = (sender as ListBox).SelectedItem;
            var figure = figureList.Where(x => x == item).First();
            selectedFigure = figure;
            figure.isSelected = true;
            canvas.Invalidate(true);
        }

        private void RemoveFigure_Click(object sender, EventArgs e)
        {
            if (selectedFigure == null)
                return;

            DialogResult dialogResult = MessageBox.Show($"Вы уверены что хотите удалить объект {selectedFigure}?", "Подтверждение действия", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                figureList.Remove(selectedFigure);
                FiguresLB.Items.Remove(selectedFigure);
                canvas.Invalidate();
            }            
        }

        private void FiguresLB_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var figure = (sender as ListBox).SelectedItem as Figure;
            RenameFigure(figure);
        }

        private void RenameFigure(Figure figure)
        {
            var stringResult = Interaction.InputBox("Введите новое имя для фигуры", "Переименовка объекта", figure.name);

            if (String.IsNullOrWhiteSpace(stringResult))
                MessageBox.Show("Новое имя фигуры не может быть пустым!", "Ошибка");

            var isExist = figureList.Where(x => x.name == stringResult && x == figure).Any();

            if (isExist)
                MessageBox.Show("Введённое имя уже занято, выберите другое!");

            figure.name = stringResult;
            FiguresLB.Items.Clear();
            FiguresLB.Items.AddRange(figureList.ToArray());
        }

        private void canvas_DoubleClick(object sender, MouseEventArgs e)
        {
            foreach (var figure in figureList)
            {
                figure.isSelected = false;
            }

            foreach (var figure in figureList)
            {
                if (figure.IsInside(e.X, e.Y))
                {
                    figure.isSelected = true;
                    RenameFigure(figure);
                    break;
                }
            }
        }
    }
}
