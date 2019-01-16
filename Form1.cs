using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pathfinder1._0
{   // Summary:
    //     Does some really cool stuff
    //
    public partial class Form1 : Form
    {
        static int sizeOfArrays = 7;
        Node[] nodes=new Node[sizeOfArrays];
        Boolean[] green = new Boolean[sizeOfArrays];
        public Form1()
        {
            InitializeComponent();
            
        }
        /*
         * 
         * The load class
         */
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (nodes==null)
            {
                return;
            }
            Graphics palette = e.Graphics;
            Color greyish = new Color();
            greyish = Color.FromArgb(181, 184, 188);
            SolidBrush brush = new SolidBrush(greyish);
            Pen blackpen = new Pen(Color.Black);
            int width = ((int)palette.DpiX / 2) / ((nodes.Length%2)+1);
            int i = 0;
            palette.FillRectangle(brush, 0, 0, palette.DpiX, palette.DpiY);
            brush.Color = Color.Red;
            while (i < ((nodes.Length / 2)+1))
            {
                if (i == 0)
                {
                    palette.FillEllipse(brush,width * i, (int)palette.DpiY / 2, width, width);
                    if (nodes.Length > 1)
                    {
                        palette.DrawLine(blackpen, (width * i)+(width/2),(palette.DpiY / 2)+(width/2), (width * (i+1))+(width/2), (((int)palette.DpiY / 2) + width)+(width/2));
                        palette.DrawLine(blackpen, (width * i) + (width / 2), (palette.DpiY / 2) + (width / 2), (width * (i + 1))+(width/2), (((int)palette.DpiY / 2) - width)+(width/2));
                    }
                }
                else
                {
                    if (i!=(nodes.Length / 2))
                    {
                        palette.DrawLine(blackpen, (width * i) + (width / 2), ((palette.DpiY / 2) + width) + (width / 2), (width * (i + 1)) + (width / 2), (((int)palette.DpiY / 2) + width) + (width / 2));
                        palette.DrawLine(blackpen, (width * i) + (width / 2), ((palette.DpiY / 2) + width) + (width / 2), (width * (i + 1)) + (width / 2), (((int)palette.DpiY / 2) - width) + (width / 2));
                        palette.DrawLine(blackpen, (width * i) + (width / 2), ((palette.DpiY / 2) - width) + (width / 2), (width * (i + 1)) + (width / 2), (((int)palette.DpiY / 2) - width) + (width / 2));
                        palette.DrawLine(blackpen, (width * i) + (width / 2), ((palette.DpiY / 2) - width) + (width / 2), (width * (i + 1)) + (width / 2), (((int)palette.DpiY / 2) + width) + (width / 2));

                    }
                    palette.FillEllipse(brush, width * i, ((int)palette.DpiY / 2)+width, width, width);
                    palette.FillEllipse(brush, width * i, ((int)palette.DpiY / 2)- width, width, width);
                    if (green[(i * 2) - 1])
                    {
                        brush.Color = Color.Green;
                        palette.FillEllipse(brush, width * i, ((int)palette.DpiY / 2) - width, width, width);
                        brush.Color = Color.Red;
                    }
                    else if(green[(i * 2)])
                    {

                        brush.Color = Color.Green;
                        palette.FillEllipse(brush, width * i, ((int)palette.DpiY / 2) + width, width, width);
                        brush.Color = Color.Red;

                    }


                }
                i++;

            }
            brush.Dispose();
            palette.Dispose();


        }
        private void run_Path(object sender, EventArgs e)
        {
            //
            //VARIABLE SETUP
            //
            int size = nodes.Length;
            Random randomGen = new Random();
            String append;
            int[] legs = new int[(size-1) *2];
            int i = 0;
            int pathcounter = 0;
            int buffer = 0;
            int[] path = new int[(nodes.Length - (nodes.Length % 2)) / 2];
            int[] pathsLastsandBests = new int[3];
            //
            //CREATE EDGES, DISPALY
            //
            
            i = 0;
            for (i = 0; i < green.Length; i++)
            {
                green[i] = false;
            };
            i = 0;
            for (i = 0; i < nodes.Length-2; i++)
            {
                nodes[i] = new Node();
                nodes[i].edges = new Edge[2];
                nodes[i].edges[0] = new Edge(randomGen.Next(1,10));
                nodes[i].edges[1] = new Edge(randomGen.Next(1,10));
            }
            i = 0;
            while (i < nodes.Length - 2)
            {
                if (i==0 || i % 2 == 0)
                {
                    buffer = 1;
                }
                else
                {
                    buffer = 2;
                }
                append = nodes[i].edges[0].weight+" between nodes "+i+" and "+ (i+buffer);
                append += ". "+ nodes[i].edges[1].weight + " between nodes " + i + " and " + (i +buffer+1)+"\r\n";
                textBox1_TextChanged(sender,e,append);
                i++;
            }
            //
            //FILL UP LEGS
            //
            i = (size-1) * 2;
            for (i = (size - 1) * 2; i > 4; i-=2)
            {
                Debug.WriteLine("" + i);
                if ((i/2) % 2 == 0)
                {
                    buffer = 2;
                }
                else
                {
                    buffer = 1;
                }
                Debug.WriteLine(((i/2)-(buffer+1))+" " + nodes[(i / 2) - (buffer + 1)].edges[buffer - 1].weight + " "+ ((i / 2) - buffer)+" " + nodes[(i / 2) - buffer].edges[buffer - 1].weight);
                if (nodes[i/2-(buffer+1)].edges[buffer-1].weight > nodes[i / 2 - buffer].edges[buffer-1].weight)
                {
                    legs[i-1] = nodes[(i / 2) - buffer].edges[buffer-1].weight;
                    legs[i - 2] = (i / 2) - buffer;
                    
                }
                else
                {
                    legs[i-1] = nodes[(i / 2) - (buffer+1)].edges[buffer-1].weight;
                    legs[i - 2] = (i / 2) - (buffer+1);
                    //
                    //Edit GREEN
                    //
                    if (i - 2 > 0)
                    {
                        green[(i / 2) - (buffer + 1)] = true;
                    }

                }
                
                
            }
            legs[3] = nodes[0].edges[1].weight;
            legs[1] = nodes[0].edges[0].weight;
            legs[0] = 0;
            legs[2] = 0;
            //PRINT GREEN
            i = 0;
            Debug.Write("\n");
            while (i < green.Length)
            {
                if (green[i])
                {
                    Debug.Write("true ");
                }
                else
                {
                    Debug.Write("False ");
                }
                
                i++;
            }
            Debug.Write("\n");
            i = 0;
            //Finish printing Green

            //
            //Calculate Best
            //
            Debug.WriteLine("Legs filled");
            i = 0;
            while (i < legs.Length)
            {
                Debug.Write(""+legs[i]);
                i++;
            }
            i = 0;
            while (i < legs.Length)
            {
                append = "" + legs[i];
                textBox1_TextChanged(sender, e, append);
                i++;
            }
            append = "\r\n";
            textBox1_TextChanged(sender, e, append);
            Debug.WriteLine("");
            i = (nodes.Length-1);
            //Reuse buffer as step counter and size as total
            buffer = 0;
            size = 0;
            while (i > 0)
                
            {
                Debug.Write("Backchecking " + i+ "\n");
                append = "";
                buffer = i*2;
                pathsLastsandBests[0] = 0;
                pathcounter =0;
                size = 0;
                while (size < path.Length)
                {
                    path[size] = 0;
                    size++;
                }
                size = 0;
                append = "To get to " + i  + " From node 0, you go to ";
                path[pathcounter] = i;
                pathcounter++;
                while (buffer > 0)
                {
                    Debug.Write("Buffer " + buffer + "\n");
                    size += legs[buffer-1];
                    if (legs[buffer - 2] != 0)
                    {
                        Debug.Write("Legs i-2 =" + legs[buffer-2] + "\n");
                        if(legs[buffer - 2] != 0)
                        {
                            path[pathcounter] = legs[buffer - 2];
                            pathcounter++;
                        }
                        
                        pathsLastsandBests[0]++;
                        Debug.Write("Path " +(pathcounter) + " is "+ legs[buffer-2]);
                        buffer = legs[buffer- 2]*2;
                    }
                    else
                    {
                        while (pathcounter>1)
                        {
                            Debug.Write("Path " + (pathcounter-1) + " is " + path[pathcounter-1]);
                            append += "node "+path[pathcounter-1]+", then ";
                            pathcounter--;
                            //
                            //Edit GREEN
                            //
                           
                           green[path[pathcounter-1]] = true;
                           this.Refresh();
                        }
                        this.Refresh();
                        append += "node"+path[0]+"\r\n";
                        append += "For a total of " + size+"\r\n";
                        textBox1_TextChanged(sender, e, append);
                        if (i == (nodes.Length - 1) * 2)
                        {
                            pathsLastsandBests[2] = size;
                        }
                        if (i == (nodes.Length - 1) * 2)
                        {
                            pathsLastsandBests[1] = size;
                        }
                        buffer = 0;
                    }
                    
                }
                i --;
            }
            if (pathsLastsandBests[1] > pathsLastsandBests[2])
            {
                append = "Best path via " + (nodes.Length - 2) + " for a total of " + pathsLastsandBests[2] + "\r\n";
                textBox1_TextChanged(sender, e, append);
            }
            else
            {
                append = "Best path via " + (nodes.Length - 1) + " for a total of " + pathsLastsandBests[1] + "\r\n";
                textBox1_TextChanged(sender, e, append);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e,String append)
        {
            textOutput.Text += append;
        }
    }
}
