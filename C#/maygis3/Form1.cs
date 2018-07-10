using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapGIS.GeoDataBase;
using MapGIS.GISControl;
using MapGIS.GeoMap;
namespace maygis3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //定义数据源
            Server svr = new Server();
            //连接数据源
            if (svr.Connect("MapGISLocal", "", "")) ;
            {
                //打开数据库
                DataBase gdb = svr.OpenGDB("sample");
                if (gdb != null)
                {
                    //打开简单要素类
                    SFeatureCls sfcls = new SFeatureCls(gdb);
                    if (sfcls.Open("等值线", 0))
                    {
                        MessageBox.Show("读取数据成功");
                        //显示简单要素类,实例地图显示控件
                        //实例化地图显示控件
                        MapControl mapCtrl = new MapControl();
                        mapCtrl.Dock = DockStyle.Fill;
                        //添加控件
                        this.splitContainer1.Panel2.Controls.Add(mapCtrl);
                        //创建图层
                        VectorLayer layer = new VectorLayer(VectorLayerType.SFclsLayer);
                        if (layer.AttachData(sfcls))
                        {
                            Map map = new Map();
                            map.Append(layer);
                            mapCtrl.ActiveMap = map;
                            //复位显示地图
                            mapCtrl.Restore();
                        }

                    }
                }
            }
        }
    }
}
