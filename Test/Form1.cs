using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Output;
using TechnikSys.MoldManager.NX.PartList;
using TechnikSys.MoldManager.NX.CAM;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 读取txt文件内容
        /// </summary>
        /// <param name="Path">文件地址</param>
        public string ReadTxtContent(string Path)
        {
            StreamReader sr = new StreamReader(Path, Encoding.GetEncoding("gb2312"));
            string content1="";
            string content;
            while ((content = sr.ReadLine()) != null)
            {
                content1 = content1 + content.ToString();
            }
            return content1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string strFileName = ofd.FileName;
                textBox1.Text = strFileName;
            }
            //string test = "测试";
            //string num = "123556";
            //test = test.ToUpper();
            //num = num.ToUpper();
            //test = test.ToLower();
            //num = num.ToLower();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                //string PartList = ReadTxtContent(textBox1.Text);
                string Entity = ReadTxtContent(textBox1.Text);
                //List<Part> parts = new List<Part>()
                //{
                //    new Part {PartID=0,ProjectID=0,ShortName="数据测试",Name="116161_Black(4212+4213)_9003_V00",Specification="43.00X15.57X32.46",CreateDate=Convert.ToDateTime("0001-01-01T00:00:00"),ReleaseDate=Convert.ToDateTime("0001-01-01T00:00:00")
                //    ,MaterialName="Unimax",Hardness="HRC54-56",BrandName="南烽",SupplierName="",Memo="",Quantity=1,Enabled=true,Version="00",TotalQty=1,Virtual=true,JobNo="9003",PartNumber="116161-9003"},
                //    //new Part {PartID=0,ProjectID=10,ShortName="Name2",Name="116161_Hot runner_1091_V20",PartListID=0 },
                //};
                //string str = JsonConvert.SerializeObject(parts);
                //List<Part> _parts = JsonConvert.DeserializeObject<List<Part>>(str);
                MGSetting _entity = JsonConvert.DeserializeObject<MGSetting>(Entity);
                //UGParts ugp = new UGParts("localhost", "50187");
                MGInformation mgi = new MGInformation("localhost", "50187");
                //ResponseInfo ri = ugp.SaveParts(_parts);
                int ri = mgi.AddOrUpdateMGDrawing(_entity);
                //string version = "011";
                //version = version.Substring(version.Count()-2,2);
                //MessageBox.Show(version);

            }
            else
            {
                MessageBox.Show("！");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UGParts ugp = new UGParts("localhost", "50187");
            List<Part> _parts = ugp.GetPartsByMoldVer("110043",true,1);
            string filePath = Directory.GetCurrentDirectory() + "\\"  + "Parts.txt";
            if (File.Exists(filePath))
                File.Delete(filePath);
            FileStream fs = new FileStream(filePath, FileMode.Create);
            string str = JsonConvert.SerializeObject(_parts);
            byte[] data = System.Text.Encoding.Default.GetBytes(str);
            //开始写入
            fs.Write(data, 0, data.Length);
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MGSetting _entity = new MGSetting { DrawName= "DrawName", CADNames = "CADNames", Qty =1, ProcessType =1, Time =18.20M, FeatureNote ="tttt", Material = "Material", HRC= "HRC", RawSize= "RawSize" ,Rev=0,ReleaseFlag=true,MoldName="111888"};
            MGInformation mgi = new MGInformation("localhost", "50187");
            int ri = mgi.AddOrUpdateMGDrawing(_entity);
        }
    }
}
