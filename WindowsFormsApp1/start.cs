using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Speech;
using Microsoft.Speech.Synthesis;
using MySql.Data.MySqlClient;
using MySql.Data;
using Tobii.Research;
using Tobii.Research.Addons;
using Tobii.Research.CodeExamples;
using System.IO;
using CsvHelper;
using System.Drawing.Imaging;
using CliWrap;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using Org.BouncyCastle.Crypto;
using System.Security.Cryptography.Xml;
using System.EnterpriseServices;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Timers;


namespace WindowsFormsApp1
{
    public partial class start : Form
    {
        // Eye Tracker Data
        public IEyeTracker eyeTracker;
        public List<Point> Gazes;
        int count = 0;
        String strConn = "Server=localhost;Database=dyslexia;Uid=root;Pwd=7350;";
        MySqlConnection conn;
        MySqlCommand cmd;
        Form f;
        


        public start(Management form, string text)
        {
            InitializeComponent();
            initailizeDB();
            initailizeSQLDB();
            textBox1.Text = text;
            label1.Text = text;
            f = form;
            try
            {
                eyeTracker = EyeTrackingOperations_GetEyeTracker_String.GetEyeTracker("tet-tcp://172.28.195.1"); //Connect Eyetracker
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                MessageBox.Show("아이트래커 연결 실패");
            }
        }

        public static void ResetGazeData()
        {
            var exec = new Cli("cmd")
                .SetArguments("/c del gaze-data.csv")
                .Execute();
        }

        class GDI32
        {
            public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter

            [DllImport("gdi32.dll")]
            public static extern long BitBlt(IntPtr hdcDest, int nXDest, int nYDest,
                int nWidth, int nHeight, IntPtr hdcSrc,
                int nXSrc, int nYSrc, int dwRop);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
                int nHeight);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);
            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        }

        /// <summary>
        /// Helper class containing User32 API functions
        /// </summary>
        class User32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }

            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);
            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
        }

        public static Image memoryImage;
        public static void CaptureScreen(Panel scr)
        {
            // get te hDC of the target window
            IntPtr hdcSrc = User32.GetWindowDC(scr.Handle);
            // get the size
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(scr.Handle, ref windowRect);
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;
            // create a device context we can copy to
            IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
            // create a bitmap we can copy it to,
            // using GetDeviceCaps to get the width/height
            IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
            // select the bitmap object
            IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
            // bitblt over
            GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);
            // restore selection
            GDI32.SelectObject(hdcDest, hOld);
            // clean up
            GDI32.DeleteDC(hdcDest);
            User32.ReleaseDC(scr.Handle, hdcSrc);
            // get a .NET image object for it
            memoryImage = Image.FromHbitmap(hBitmap);
            //memoryImage.Save(@"screenShot.png", ImageFormat.Png);
            // free up the Bitmap object
            GDI32.DeleteObject(hBitmap);
        }

        public static void ScreenCapture()
        {
            int width = 1440;
            int height = 900;

            Bitmap bitmap = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bitmap);

            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(width, height));

            g.Dispose();
            g = null;

            bitmap.Save(@"screenShot.png", ImageFormat.Png);

            // 2nd Method
            /*Rectangle rect = Screen.PrimaryScreen.Bounds;

            int bitsPerPixel = Screen.PrimaryScreen.BitsPerPixel;
            PixelFormat pixelFormat = PixelFormat.Format32bppArgb;
            if (bitsPerPixel <= 16)
            {
                pixelFormat = PixelFormat.Format16bppRgb565;
            }
            if (bitsPerPixel == 24)
            {
                pixelFormat = PixelFormat.Format24bppRgb;
            }

            Bitmap bmp = new Bitmap(rect.Width, rect.Height, pixelFormat);

            using (Graphics gr = Graphics.FromImage(bmp))
            {
                gr.CopyFromScreen(rect.Left, rect.Top, 0, 0, rect.Size);
            }

            bmp.Save(@"screenShot.png", ImageFormat.Png);
            bmp.Dispose();*/
        }

        public static void GazeHeatPlot()
        {
            //var exec = new Cli("cmd")
            //    .SetArguments("/c python gazeheatplot.py gaze-data.csv 1440 900 -a 0.6 -o gazeheatplot.png -b screenShot.png -n 200 -sd 33")
            //    .Execute();

            var exec = new Cli("cmd")
                .SetArguments("/c python gazeheatplot.py gaze-data.csv 1440 900 -a 0.6 -o gazeheatplot.png -b bg-image.png -n 200 -sd 33")
                .EnableExitCodeValidation(false) // disables exceptions on non-zero exit code
                .EnableStandardErrorValidation(false) // disables exceptions on non-empty stderr
                .ExecuteAsync();
        }

        public static void OpenGazeHeatPlot()
        {
            var exec = new Cli("cmd")
                .SetArguments("/c gazeheatplot.png")
                .Execute();
        }

        class IEyeTracker_GazeDataReceived
        {

            static IEyeTracker eyetracker;
            static Point gaze_data = new Point(0, 0);
            static Point avg_gaze;
            static int W = Screen.PrimaryScreen.Bounds.Width;
            static int H = Screen.PrimaryScreen.Bounds.Height;
            static int cnt = 0;
            static Form f;
            static List<Point> Analysis = new List<Point>();
            static int count = 0;
            static String strConn = "Server=localhost;Database=dyslexia;Uid=root;Pwd=7350;";
            static MySqlConnection conn;
            static MySqlCommand cmd;
            public static void Execute(IEyeTracker eyeTracker, Form form)
            {
                if (eyeTracker != null)
                {
                    eyetracker = eyeTracker;
                    GazeDataStart(eyetracker);
                    f = form;

                    //MakeColumn(ds);
                }
            }
            // <BeginExample>
            private static void GazeDataStart(IEyeTracker eyeTracker)
            {
                ResetGazeData();

                eyeTracker.GazeDataReceived += EyeTracker_GazeDataReceived;
                //System.Threading.Thread.Sleep(2000);
                /*
                DataSet ds = new DataSet();
                String strConn = "Server=localhost;Database=dyslexia;Uid=root;Pwd=7350;";
                MySqlConnection conn;
                MySqlCommand cmd;

                

                conn = new MySqlConnection(strConn);
                conn.Open();
                string sql = "SELECT max FROM resultset";
                MySqlDataAdapter adpt = new MySqlDataAdapter(sql, conn);
                adpt.Fill(ds);
                Console.WriteLine(ds.Tables[0].Rows);
                System.Diagnostics.Debug.WriteLine(ds.Tables[0].Rows);
                System.Diagnostics.Debug.WriteLine(adpt);
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Console.WriteLine(r["Max"]);
                }
                //cmd = new MySqlCommand(sql, conn);
                //MySqlDataReader rdr = cmd.ExecuteReader();

                //cmd.ExecuteNonQuery();
                conn.Close();

                */
            }
            public static void GazeDataEnd()
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                List<String> list = new List<String>();
                String strConn = "Server=localhost;Database=dyslexia;Uid=root;Pwd=7350;";
                MySqlConnection conn;
                
                /*
                var writer = new StreamWriter("gaze-data.csv", true);
                using (var csv = new CsvWriter(writer))
                {
                    //csv.WriteField(gaze_data.X);
                    //csv.WriteField(gaze_data.Y);
                    csv.NextRecord();
                }
                */
                eyetracker.GazeDataReceived -= EyeTracker_GazeDataReceived;
                /*
                try //DB에서 positionX, positionY, time불러와서 리스트에 넣기
                {
                    conn = new MySqlConnection(strConn);
                    conn.OpenAsync();
                    string sql = "SELECT * FROM resultset";
                    Console.WriteLine(sql);
                    //MySqlCommand command = new MySqlCommand("SELECT * FROM resultset", conn);
                    //MySqlDataReader reader = command.ExecuteReader();
                    MySqlDataAdapter adpt = new MySqlDataAdapter(sql, conn);
                    adpt.Fill(ds);



                    Dao dao = new Dao();
                    DataReader datareader = new DataReader();
                    //datareader.GetDatas();

                    string[][] datas = DataReader.GetDatas(sql);

                    Console.WriteLine("data : " + datas);


                    foreach (DataRow r in ds.Tables[0].Rows)
                    {

                        Console.WriteLine("positionX : " + r["positionX"]);
                        Console.WriteLine("positionY : " + r["positionY"]);
                        Console.WriteLine("time : " + r["time"]);
                        //list.Add(r.Table.Rows.ToString());

                        list.Add(r["positionX"].ToString());
                        list.Add(r["positionY"].ToString());
                        list.Add(r["time"].ToString());
                        Console.WriteLine("list : " + list);
                        Console.WriteLine("list : " + list[0]);
                    }
                    conn.Close();


                }
                catch (Exception e){ Console.WriteLine(e.StackTrace); }
                */
                
                try//변형데이터
                {
                    conn = new MySqlConnection(strConn);
                    conn.Open();

                    string sql = "INSERT INTO transformresult(`fixation1`, `fixation2`, `fixation3`, `fixation4`, `fixation5`, `fixation6`, `fixation7`, `fixation8`, `saccade1`, `saccade2`, `saccade3`, `saccade4`, `saccade5`, `saccade6`, `saccade7`, `totaltime`)" +
                        "VALUES ((SELECT time FROM resultset WHERE positionX > 302 && positionX < 384 ORDER BY time DESC limit 1) - (SELECT time FROM resultset WHERE positionX > 302 && positionX < 384 ORDER BY time ASC limit 1)," +
                        "(SELECT time FROM resultset WHERE positionX > 393 && positionX < 470 ORDER BY time DESC limit 1) - (SELECT time FROM resultset WHERE positionX > 393 && positionX < 470 ORDER BY time ASC limit 1)," +
                        "(SELECT time FROM resultset WHERE positionX > 490 && positionX < 565 ORDER BY time DESC limit 1) - (SELECT time FROM resultset WHERE positionX > 490 && positionX < 565 ORDER BY time ASC limit 1)," +
                        "(SELECT time FROM resultset WHERE positionX > 576 && positionX < 665 ORDER BY time DESC limit 1) -(SELECT time FROM resultset WHERE positionX > 576 && positionX < 665 ORDER BY time ASC limit 1)," +
                        "(SELECT time FROM resultset WHERE positionX > 724 && positionX < 803 ORDER BY time DESC limit 1) - (SELECT time FROM resultset WHERE positionX > 724 && positionX < 803 ORDER BY time ASC limit 1)," +
                        "(SELECT time FROM resultset WHERE positionX > 817 && positionX < 895 ORDER BY time DESC limit 1) - (SELECT time FROM resultset WHERE positionX > 817 && positionX < 895 ORDER BY time ASC limit 1)," +
                        "(SELECT time FROM resultset WHERE positionX > 909 && positionX < 986 ORDER BY time DESC limit 1) - (SELECT time FROM resultset WHERE positionX > 909 && positionX < 986 ORDER BY time ASC limit 1)," +
                        "(SELECT time FROM resultset WHERE positionX > 1005 && positionX < 1080 ORDER BY time DESC limit 1) - (SELECT time FROM resultset WHERE positionX > 1005 && positionX < 1080 ORDER BY time ASC limit 1)," +
                        "(SELECT time FROM resultset WHERE positionX > 384 && positionX < 393 ORDER BY time DESC limit 1) -(SELECT time FROM resultset WHERE positionX > 383 && positionX < 393 ORDER BY time ASC limit 1)," +
                        "(SELECT time FROM resultset WHERE positionX > 470 && positionX < 490 ORDER BY time DESC limit 1) -(SELECT time FROM resultset WHERE positionX > 470 && positionX < 490 ORDER BY time ASC limit 1)," +
                        "(SELECT time FROM resultset WHERE positionX > 565 && positionX < 576 ORDER BY time DESC limit 1) -(SELECT time FROM resultset WHERE positionX > 565 && positionX < 576 ORDER BY time ASC limit 1)," +
                        "(SELECT time FROM resultset WHERE positionX > 665 && positionX < 724 ORDER BY time DESC limit 1) -(SELECT time FROM resultset WHERE positionX > 665 && positionX < 724 ORDER BY time ASC limit 1)," +
                        "(SELECT time FROM resultset WHERE positionX > 803 && positionX < 817 ORDER BY time DESC limit 1) -(SELECT time FROM resultset WHERE positionX > 803 && positionX < 817 ORDER BY time ASC limit 1)," +
                        "(SELECT time FROM resultset WHERE positionX > 895 && positionX < 909 ORDER BY time DESC limit 1) -(SELECT time FROM resultset WHERE positionX > 895 && positionX < 909 ORDER BY time ASC limit 1)," +
                        "(SELECT time FROM resultset WHERE positionX > 986 && positionX < 1005 ORDER BY time DESC limit 1) -(SELECT time FROM resultset WHERE positionX > 986 && positionX < 1005 ORDER BY time ASC limit 1)," +
                        "(SELECT time FROM resultset WHERE positionX > 302 && positionX < 1080 ORDER BY time DESC limit 1) -(SELECT time FROM resultset WHERE positionX > 302 && positionX < 1080 ORDER BY time ASC limit 1))";
                    cmd = new MySqlCommand(sql, conn);
                    Console.WriteLine(sql);
                    Console.WriteLine(cmd);
                    cmd.ExecuteNonQuery();

                    /*
                    //엑셀에 field 넣기
                    var lines = new List<string>();

                    string[] columnNames = dt.Columns.Cast<DataColumn>().
                                                      Select(column => column.ColumnName).
                                                      ToArray();

                    var header = string.Join(",", columnNames);
                    lines.Add(header);

                    var valueLines = dt.AsEnumerable()
                                       .Select(row => string.Join(",", row.ItemArray));
                    lines.AddRange(valueLines);

                    File.WriteAllLines("Excel.csv", lines);
                    */

                    conn.Close();
                }
                catch (Exception ex) { Console.WriteLine(ex.StackTrace); }
                //TRUNCATE TABLE transformresult;
                //TRUNCATE TABLE resultset;
                //GazeHeatPlot();
                //OpenGazeHeatPlot();
                

            }
            
            public static void saveTable()
            {
                DataSet ds = new DataSet();
                String strConn = "Server=localhost;Database=dyslexia;Uid=root;Pwd=7350;";
                using (MySqlConnection conn = new MySqlConnection(strConn))
                {
                    string query = "SELECT " +
                   "*FROM " +
                   "transformresult";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 120;

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    try
                    {
                        conn.Open();
                        da.Fill(ds);
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        conn.Close();
                    }
                }


                StreamWriter sw = new StreamWriter("CSV.csv");
                WriteToStream(sw, ds.Tables[0], false, false);

                Console.ReadKey();
                /*
                DataTable dt = new DataTable();

                //파일 저장 위치 선택.
                SaveFileDialog saveDlg = new SaveFileDialog();
                saveDlg.InitialDirectory = System.Environment.CurrentDirectory;
                saveDlg.Filter = "csv (*.csv)|*.csv|txt (*txt)|*.txt|All files (*.*)|*.*";
                if (saveDlg.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }

                //파일 저장을 위해 스트림 생성.
                FileStream fs = new FileStream(saveDlg.FileName, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

                //컬럼 이름들을 ","로 나누고 저장.
                string line = string.Join(",", dt.Columns.Cast<object>());
                sw.WriteLine(line);

                //row들을 ","로 나누고 저장.
                foreach (DataRow item in dt.Rows)
                {
                    line = string.Join(",", item.ItemArray.Cast<object>());
                    sw.WriteLine(line);
                }

                sw.Close();
                fs.Close();
                */
            }
            

            public static void WriteToStream(TextWriter stream, DataTable table, bool header, bool quoteall)
            {
                if (header)
                {
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        WriteItem(stream, table.Columns[i].Caption, quoteall);
                        if (i < table.Columns.Count - 1)
                            stream.Write(',');
                        else
                            stream.Write("\r\n");
                    }
                }
                foreach (DataRow row in table.Rows)
                {
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        WriteItem(stream, row[i], quoteall);
                        if (i < table.Columns.Count - 1)
                            stream.Write(',');
                        else
                            stream.Write("\r\n");
                    }
                }
                stream.Flush();
                stream.Close();
            }

            private static void WriteItem(TextWriter stream, object item, bool quoteall)
            {
                if (item == null)
                    return;
                string s = item.ToString();
                if (quoteall || s.IndexOfAny("\",\x0A\x0D".ToCharArray()) > -1)
                    stream.Write("\"" + s.Replace("\"", "\"\"") + "\"");
                else
                    stream.Write(s);
                stream.Flush();
            }

            public static void EyeTracker_GazeDataReceived(object sender, GazeDataEventArgs e)
            {
                //String strConn = "Server=localhost;Database=dyslexia;Uid=root;Pwd=7350;";
                //MySqlConnection conn;
                //MySqlCommand cmd;

                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                List<String> list = new List<String>();
                List<String> resultList = new List<String>();
                DateTime time = DateTime.Now;
                string currentTime;
                cnt++;
                gaze_data.X += (int)(((e.LeftEye.GazePoint.PositionOnDisplayArea.X * W) + (e.RightEye.GazePoint.PositionOnDisplayArea.X * W)) / 2);
                gaze_data.Y += (int)(((e.LeftEye.GazePoint.PositionOnDisplayArea.Y * H) + (e.RightEye.GazePoint.PositionOnDisplayArea.Y * H)) / 2);
                /*
                var writer = new StreamWriter("gaze-data.csv", true);
                
                using (var writer = new StreamWriter("gaze-data.csv"))
                using (var csv = new CsvWriter(writer))
                if (gaze_data.X > 0 && gaze_data.Y > 0)
                {
                    csv.WriteField(gaze_data.X);
                    csv.WriteField(gaze_data.Y);
                    csv.NextRecord();
                }
                */
                /**
                 Start 버튼을 누르면
                트래킹을 시작하여 X, Y, Time값을 뽑아내고
                뽑아낸 값을 INSERT문을 통해 DB에 저장하고
                모두 저장이 되면 변형 데이터를 만든다.
                 */
                if (cnt >= 5) //Raw data INSERT (X, Y, Time)
                {
                    avg_gaze.X = (int)gaze_data.X / cnt;
                    avg_gaze.Y = (int)gaze_data.Y / cnt;
                    currentTime = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
                    Analysis.Add(avg_gaze);
                    Console.WriteLine(avg_gaze);
                    Console.WriteLine(currentTime);

                    String str_avg_gazeX = avg_gaze.X.ToString();
                    String str_avg_gazeY = avg_gaze.Y.ToString();

                    resultList.Add(str_avg_gazeX);
                    resultList.Add(str_avg_gazeY);
                    resultList.Add(currentTime);

                    for (int i = 0; i < resultList.Count; i++)
                    {
                        Console.WriteLine("{0}번 째 list 값: {1}",i, resultList[i]);
                    }
                    
                    
                    try 
                    {
                        //excel로 넣기
                        /*
                        var writer = new StreamWriter("gaze-data.csv", true);
                        
                        using (var csv = new CsvWriter(writer))
                        if (avg_gaze.X > 0 && avg_gaze.Y > 0)
                        {
                            csv.WriteField(avg_gaze.X);
                            csv.WriteField(avg_gaze.Y);
                            csv.WriteField(currentTime);
                            csv.NextRecord();

                            var lines = new List<string>();

                            string[] columnNames = dt.Columns.Cast<DataColumn>().
                                                              Select(column => column.ColumnName).
                                                              ToArray();

                            var header = string.Join(",", columnNames);
                            lines.Add(header);

                            var valueLines = dt.AsEnumerable()
                                               .Select(row => string.Join(",", row.ItemArray));
                            lines.AddRange(valueLines);

                            File.WriteAllLines("Excel.csv", lines);
                        }*/

                        
                        if (avg_gaze.X > 0 && avg_gaze.Y > 0)
                        {
                            
                            conn = new MySqlConnection(strConn);
                            conn.OpenAsync();
                            string sql = "INSERT INTO resultset (positionX, positionY, time)" +
                            "VALUES (" + avg_gaze.X + "," + avg_gaze.Y + "," + "NOW()" + ")";
                            cmd = new MySqlCommand(sql, conn);
                            Console.WriteLine(sql);
                            Console.WriteLine(cmd);

                            cmd.ExecuteNonQuery();
                            /*
                            var lines = new List<string>();

                            string[] columnNames = dt.Columns.Cast<DataColumn>().
                                                              Select(column => column.ColumnName).
                                                              ToArray();

                            var header = string.Join(",", columnNames);
                            lines.Add(header);

                            var valueLines = dt.AsEnumerable()
                                               .Select(row => string.Join(",", row.ItemArray));
                            lines.AddRange(valueLines);

                            File.WriteAllLines("Excel.csv", lines);
                            */

                            conn.Close();
                        }
                        

                    }
                    catch (Exception ex) { Console.WriteLine(ex.StackTrace); }

                    /*
                    try//변형데이터
                    {
                        conn = new MySqlConnection(strConn);
                        conn.Open();

                        string sql = "INSERT INTO transformresult(`fixation`, `fixation2`, `saccade`, `totaltime`)" +
                            "VALUES ((SELECT time FROM resultset WHERE positionX > 384 && positionX < 500 ORDER BY time DESC limit 1) - (SELECT time FROM resultset WHERE positionX > 384 && positionX < 500 ORDER BY time ASC limit 1)," +
                            "(SELECT time FROM resultset WHERE positionX > 610 && positionX < 907 ORDER BY time DESC limit 1) -(SELECT time FROM resultset WHERE positionX > 610 && positionX < 907 ORDER BY time ASC limit 1)," +
                            "(SELECT time FROM resultset WHERE positionX > 498 && positionX < 612 ORDER BY time DESC limit 1) -(SELECT time FROM resultset WHERE positionX > 498 && positionX < 612 ORDER BY time ASC limit 1)," +
                            "(SELECT time FROM resultset WHERE positionX > 384 && positionX < 907 ORDER BY time DESC limit 1) -(SELECT time FROM resultset WHERE positionX > 384 && positionX < 907 ORDER BY time ASC limit 1))";
                        cmd = new MySqlCommand(sql, conn);
                        Console.WriteLine(sql);
                        Console.WriteLine(cmd);
                        cmd.ExecuteNonQuery();
                        conn.Clghose();
                    }
                    catch (Exception ex){ Console.WriteLine(ex.StackTrace);}
                    */
                    /*
                     앞에서 생성된 데이터 중 시간값을 통해 고정시간,도약시간,전체응시시간을 만든다.
                     */
                    drawGaze(avg_gaze);
                    cnt = 0;
                    gaze_data = new Point(0, 0);
                    avg_gaze = new Point(0, 0);

                }



            }

            private static string SYSDATE()
            {
                throw new NotImplementedException();
            }

            private static void saccade()
            {

            }

            private static void drawGaze(Point avg_gaze)
            {
                Graphics g;
                g = f.CreateGraphics();

                Rectangle rectangle = new Rectangle();
                PaintEventArgs arg = new PaintEventArgs(g, rectangle);

                DrawCircle(arg, avg_gaze.X, avg_gaze.Y, 5, 5);
                //f.Refresh();
            }
            private static void DrawCircle(PaintEventArgs e, int x, int y, int width, int height)
            {
                Pen pen = new Pen(Color.Blue, 5);
                e.Graphics.DrawEllipse(pen, x - width / 2, y - height / 2, width, height);
            }
            public static void ClearPoints()
            {
                Analysis.Clear();
            }
            public static List<Point> GetPoints()
            {
                return Analysis;
            }
            // <EndExample>
        }

        public class Dao
        {
            public static String strConn = "Server=localhost;Database=dyslexia;Uid=root;Pwd=7350;";
            public static MySqlDataAdapter da;
            public static DataSet ds;
            public static DataTable dt;

            public Dao()
            {
                da = null;
                ds = null;
                dt = null;
            }
            public static void MysqlConn(string sql)
            {
                try
                {
                    da = new MySqlDataAdapter(sql, strConn); //DB 연결해서 어댑터 설정
                    ds = new DataSet(); //데이터셋 생성
                    da.Fill(ds); //연결한 어댑터를 이용해 데이터셋을 설정
                    dt = ds.Tables["resultset"]; //데이터 테이블에 데이터셋 정보를 넣음
                }
                catch (Exception ex)
                {
                    using (StreamWriter sw = new StreamWriter("./sqlerror.txt", true))
                    {
                        sw.WriteLine(ex.Message); //dql관련 문제가 생겼다면 파일로 오류메시지 저장
                    }
                }
            }

             
            public static void MysqlDisConn() //연결해제
            {
                try
                {
                    if (dt != null)
                    {
                        dt.Dispose();
                        dt = null;
                    }
                    if (ds != null)
                    {
                        ds.Dispose();
                        ds = null;
                    }
                    if (da != null)
                    {
                        da.Dispose();
                        da = null;
                    }
                }
                catch (Exception ex)
                {
                    using (StreamWriter sw = new StreamWriter("./sqlerror.txt", true))
                    {
                        sw.WriteLine(ex.Message); //dql관련 문제가 생겼다면 파일로 오류메시지 저장
                    }
                }
            }

            public static DataTable Get_DataTable(string sql) //데이터 테이블 통째로 들고 오고 싶을 때
            {
                MysqlConn(sql);
                return dt;
            }

            public static int ExecuteQuery(string sql)
            { 
                int rows = 0;
                MySqlConnection conn = null;
                MySqlCommand cmd = null;

                try
                {
                    conn = new MySqlConnection(strConn);
                    conn.OpenAsync();
                    cmd = new MySqlCommand(sql, conn);
                    rows = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    using (StreamWriter sw = new StreamWriter("./sqlerror.txt", true))
                    {
                        sw.WriteLine(ex.Message); //dql관련 문제가 생겼다면 파일로 오류메시지 저장
                    }
                }
                finally
                {
                    if (cmd != null) cmd.Dispose();
                    if (conn != null) conn.Close();
                }

                return rows;
            }
        }

        public class DataReader
        {
            public static DataTable dt; //쿼리문에 대한 가상 테이블
            //생성자
            public DataReader()
            {

            }

            //데이터 양을 알 수 없거나 많을 경우
            public static string[][] GetDatas(string sql)
            {
                string[][] datas = null;
                try
                {
                    dt = Dao.Get_DataTable(sql); //데이터 테이블 얻어옴
                    

                    int dt_rows_count = dt.Rows.Count; //데이터 테이블의 행 수 얻기
                    int dt_cols_count = dt.Columns.Count; //데이터 테이블의 컬럼 수 얻기
                    if (dt_rows_count > 0)
                    {
                        datas = new string[dt_rows_count][]; //행만큼 2차월 배열 생성
                        for (int i = 0; i < dt_rows_count; i++)
                        {
                            datas[i] = new string[dt_cols_count]; //해당 행 만큼 세부 1차원 배열 생성
                        }
                        for (int i = 0; i < dt_rows_count; i++)
                        {
                            for (int j = 0; j < dt_cols_count; j++)
                            {
                                datas[i][j] = Convert.ToString(dt.Rows[i][j]); //데이터를 생성한 배열에 저장
                            }
                        }

                        Console.WriteLine("데이터 : " + datas);
                        
                    }
                    else
                    {
                        datas = null; //0보다 작으면 null값 설정
                    }
                }
                catch (Exception ex)
                {
                    using (StreamWriter sw = new StreamWriter("./sqlerror.txt", true))
                    {
                        sw.WriteLine(ex.Message); //dql관련 문제가 생겼다면 파일로 오류메시지 저장
                    }
                }
                finally
                {
                    dt.Dispose();
                }
                return datas; //해당 sql문의 쿼리 결과를 2차원 문자열 배열로 전송
            }
        }

        public class caculate
        {
            public static void calculateTime()
            {
                
                String strConn = "Server=localhost;Database=dyslexia;Uid=root;Pwd=7350;";
                MySqlConnection conn;
                MySqlCommand cmd;
                try
                {
                    conn = new MySqlConnection(strConn);
                    conn.Open();

                    cmd = new MySqlCommand("INSERT INTO transformresult(`fixation`, `fixation2`, `saccade`, `totaltime`)" +
                        "VALUES ((SELECT time FROM resultset WHERE positionX > 384 && positionX < 500 ORDER BY time DESC limit 1) - (SELECT time FROM resultset WHERE positionX > 384 && positionX < 500 ORDER BY time ASC limit 1)," +
                        "(SELECT time FROM resultset WHERE positionX > 610 && positionX < 907 ORDER BY time DESC limit 1) -(SELECT time FROM resultset WHERE positionX > 610 && positionX < 907 ORDER BY time ASC limit 1)," +
                        "(SELECT time FROM resultset WHERE positionX > 498 && positionX < 612 ORDER BY time DESC limit 1) -(SELECT time FROM resultset WHERE positionX > 498 && positionX < 612 ORDER BY time ASC limit 1)," +
                        "(SELECT time FROM resultset WHERE positionX > 384 && positionX < 907 ORDER BY time DESC limit 1) -(SELECT time FROM resultset WHERE positionX > 384 && positionX < 907 ORDER BY time ASC limit 1))", conn);


                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                

            }
        }


        
        //private static void drawGaze(Point avg_gaze)
        //{
        //    Graphics g;
        //    g = f.CreateGraphics();

        //    Rectangle rectangle = new Rectangle();
        //    PaintEventArgs arg = new PaintEventArgs(g, rectangle);

        //    DrawCircle(arg, avg_gaze.X, avg_gaze.Y, 5, 5);
        //    //f.Refresh();
        //}
        private static void DrawCircle(PaintEventArgs e, int x, int y, int width, int height)
        {
            Pen pen = new Pen(Color.Blue, 5);
            e.Graphics.DrawEllipse(pen, x - width / 2, y - height / 2, width, height);
        }
        public static void ClearPoints()
        {
            //Analysis.Clear();
        }

        private void button1_Click(object sender, EventArgs e) //Start 버튼
        {
            IEyeTracker_GazeDataReceived.Execute(eyeTracker, this);

            
        }
        private void button2_Click(object sender, EventArgs e) //Stop 버튼
        {
            IEyeTracker_GazeDataReceived.ClearPoints();
            this.Refresh();
            IEyeTracker_GazeDataReceived.GazeDataEnd();
            IEyeTracker_GazeDataReceived.saveTable();
            //IEyeTracker_GazeDataReceived.MakeColumn();
        }
        private void start_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                //IEyeTracker_GazeDataReceived.GazeDataEnd();
                this.Close();
            }
        }

        private void start_Load(object sender, EventArgs e)
        {
            this.Text = "Collection System of Eye Movements";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void start_MouseMove(object sender, MouseEventArgs e)
        {
            //실시간좌표값X.Text = Cursor.Position.X.ToString();
            //실시간좌표값Y.Text = Cursor.Position.Y.ToString();
        }

        /*
        private void timer1_Tick(object sender, EventArgs e)
        {
            실시간좌표값X.Text = Cursor.Position.X.ToString();
            실시간좌표값Y.Text = Cursor.Position.Y.ToString();
        }
        */
    }



    //private static DateTime Delay(int MS)
    //{
    //    DateTime ThisMoment = DateTime.Now;
    //    TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
    //    DateTime AfterWards = ThisMoment.Add(duration);

    //    while (AfterWards >= ThisMoment)
    //    {
    //        System.Windows.Forms.Application.DoEvents();
    //        ThisMoment = DateTime.Now;
    //    }

    //    return DateTime.Now;
    //}

    //private void Div_text()
    //{
    //    div_text = text.Split(' ');

    //    foreach (string str in div_text)
    //    {
    //        ps.SetLabel_item(str);
    //    }
    //}

    //private void Training_Load(object sender, EventArgs e)
    //{
    //    Div_text();
    //}

} 

