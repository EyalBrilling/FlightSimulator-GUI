using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace FlightSimulatorApp.Views
{

    public partial class Joystick : UserControl
    {

        private Boolean mousePressed = false;
        
 
        private double width, length;
        private readonly Storyboard centerKnob;
        private Point p0;



      

        public Joystick()
        {
            InitializeComponent();
            Knob.MouseLeftButtonDown += Knob_MouseDown;
            Knob.MouseMove += Knob_MouseMove;
            Knob.MouseLeftButtonUp += Knob_MouseUp;
            
            centerKnob = Knob.Resources["CenterKnob"] as Storyboard;


           
        }

        private void Knob_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.mousePressed = true;
            p0 = e.GetPosition(Base);
           
            width = Base.ActualWidth - KnobBase.ActualWidth;
            length = Base.ActualHeight - KnobBase.ActualHeight;
            
            Knob.CaptureMouse();
            centerKnob.Stop();
        }

        private void Knob_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (mousePressed)
            {
                Point p1 = e.GetPosition(Base);
                Point p2 = new Point(p1.X - p0.X, p1.Y - p0.Y);

                double d = Len(p2);
                if (d >= length / 2 || d >= width / 2)
                {
                    return;
                }

               double newX = 2 * ((Math.Round(p2.X, 4) + 123.3333) / (123.3333 + 123.3333)) - 1;
               double newY = 2 * ((Math.Round(p2.Y, 4) + 123.3333) / (123.3333 + 123.3333)) - 1;

              
                knobPosition.X = p2.X;
                knobPosition.Y = p2.Y;

                (Application.Current as App).MySimulatorViewModel.ChangeCoordinates(newX, newY);


            }

        }


        private void Knob_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            mousePressed = false;
            Knob.ReleaseMouseCapture();
            centerKnob.Begin();
          
        }
        private double Len( Point p2)
        {
            return Math.Round(Math.Sqrt(p2.X * p2.X + p2.Y * p2.Y));
        }

       


        private void CenterKnob_Completed(object sender, EventArgs e)
        {

        }

    }
}