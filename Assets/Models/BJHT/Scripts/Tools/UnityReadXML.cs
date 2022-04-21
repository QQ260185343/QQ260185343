using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using Excel;
using OfficeOpenXml;
namespace YN
{
    public class UnityReadXML : MonoBehaviour
    {
        public DataSet mResultSet;
        string XMLpath = Application.streamingAssetsPath + "/万达店铺数据.xlsx";
        string XMLpath2 = Application.streamingAssetsPath + "/太原万达广场POI列表-位置信息0414.xlsx";
        public List<UnityUTMDataFromExcel> _UnityUTMDataFromExcels = new List<UnityUTMDataFromExcel>();
        public List<UnityUTMDataFromExcel> _UnityUTMDataFromExcels2 = new List<UnityUTMDataFromExcel>();
        public List<string> excalname = new List<string>();
        public GameObject Cubeprefab;
        // 初始化
        void Awake()
        {
            ReadXML1(XMLpath);
            ReadXML2(XMLpath2);
            WriteExcel("Test.xlsx", "万达数据");
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }



        void ReadXML1(string path)
        {
            FileStream mStream = File.Open(path, FileMode.Open, FileAccess.Read);
            IExcelDataReader mExcelReader = ExcelReaderFactory.CreateOpenXmlReader(mStream);
            mResultSet = mExcelReader.AsDataSet();
            if (mResultSet.Tables.Count > 0)
            {
                DataTable mSheet = mResultSet.Tables[0];
                if (mSheet.Rows.Count > 0)
                {
                    for (int i = 1; i < mSheet.Rows.Count; i++)//行
                    {
                        UnityUTMDataFromExcel unityUTMDataFromExcel = new UnityUTMDataFromExcel();
                        unityUTMDataFromExcel.ID = mSheet.Rows[i][0].ToString();
                        unityUTMDataFromExcel.Name = mSheet.Rows[i][1].ToString();
                        _UnityUTMDataFromExcels.Add(unityUTMDataFromExcel);


                    }
                }
            }

        }


        void ReadXML2(string path)
        {
            FileStream mStream = File.Open(path, FileMode.Open, FileAccess.Read);
            IExcelDataReader mExcelReader = ExcelReaderFactory.CreateOpenXmlReader(mStream);
            mResultSet = mExcelReader.AsDataSet();
            if (mResultSet.Tables.Count > 0)
            {
                DataTable mSheet = mResultSet.Tables[0];
                if (mSheet.Rows.Count > 0)
                {
                    //  for (int i = 1; i < mSheet.Rows.Count; i++)//行
                    for (int i = 1; i < 156; i++)//行
                    {
                        UnityUTMDataFromExcel unityUTMDataFromExcel = new UnityUTMDataFromExcel();
                        unityUTMDataFromExcel.ID = mSheet.Rows[i][0].ToString();
                        unityUTMDataFromExcel.Name = mSheet.Rows[i][4].ToString();
                        unityUTMDataFromExcel.UTMx = mSheet.Rows[i][11].ToString();
                        unityUTMDataFromExcel.UTMy = mSheet.Rows[i][12].ToString();
                        unityUTMDataFromExcel.UTMz = mSheet.Rows[i][13].ToString();
                        Vector3 vector3UTM = new Vector3(float.Parse(mSheet.Rows[i][11].ToString()), float.Parse(mSheet.Rows[i][12].ToString()), float.Parse(mSheet.Rows[i][13].ToString()));
                        Instantiate(Cubeprefab, UnityToUTM.UTMToUnity(vector3UTM), Quaternion.identity, gameObject.transform);
                        unityUTMDataFromExcel.Posx = UnityToUTM.UTMToUnity(vector3UTM).x.ToString();
                        unityUTMDataFromExcel.Posy = UnityToUTM.UTMToUnity(vector3UTM).y.ToString();
                        unityUTMDataFromExcel.Posz = UnityToUTM.UTMToUnity(vector3UTM).z.ToString();
                        unityUTMDataFromExcel.Floor = UnityToUTM.Floor(mSheet.Rows[i][2].ToString());
                        _UnityUTMDataFromExcels2.Add(unityUTMDataFromExcel);


                    }
                }
            }
        }





        /// <summary>
        /// 写入 Excel ; 需要添加 OfficeOpenXml.dll;
        /// </summary>
        /// <param name="excelName">excel文件名</param>
        /// <param name="sheetName">sheet名称</param>
        public void WriteExcel(string excelName, string sheetName)
        {
            //通过面板设置excel路径
            //string outputDir = EditorUtility.SaveFilePanel("Save Excel", "", "New Resource", "xlsx");

            //自定义excel的路径
            string path = Application.streamingAssetsPath + "/" + excelName;
            FileInfo newFile = new FileInfo(path);
            if (newFile.Exists)
            {
                //创建一个新的excel文件
                newFile.Delete();
                newFile = new FileInfo(path);
            }

            //通过ExcelPackage打开文件
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                //在excel空文件添加新sheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(sheetName);
                //添加列名
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Area_box_ID";
                worksheet.Cells[1, 3].Value = "Floor";
                worksheet.Cells[1, 4].Value = "Name";
                worksheet.Cells[1, 5].Value = "Tag";
                worksheet.Cells[1, 6].Value = "TenantID";
                worksheet.Cells[1, 7].Value = "UTMx";
                worksheet.Cells[1, 8].Value = "UTMy";
                worksheet.Cells[1, 9].Value = "UTMz";
                worksheet.Cells[1, 10].Value = "Posx";
                worksheet.Cells[1, 10].Value = "Posx";
                worksheet.Cells[1, 11].Value = "Posy";
                worksheet.Cells[1, 12].Value = "Posz";

                for (int i = 0; i < _UnityUTMDataFromExcels2.Count; i++)
                {
                    worksheet.Cells["A" + (i + 2).ToString()].Value = _UnityUTMDataFromExcels2[i].ID;
                    worksheet.Cells["B" + (i + 2).ToString()].Value = _UnityUTMDataFromExcels2[i].Area_box_ID;
                    worksheet.Cells["C" + (i + 2).ToString()].Value = _UnityUTMDataFromExcels2[i].Floor;
                    worksheet.Cells["D" + (i + 2).ToString()].Value = _UnityUTMDataFromExcels2[i].Name;
                    worksheet.Cells["E" + (i + 2).ToString()].Value = _UnityUTMDataFromExcels2[i].Tag;
                    worksheet.Cells["F" + (i + 2).ToString()].Value = _UnityUTMDataFromExcels2[i].TenantID;
                    worksheet.Cells["G" + (i + 2).ToString()].Value = _UnityUTMDataFromExcels2[i].UTMx;
                    worksheet.Cells["H" + (i + 2).ToString()].Value = _UnityUTMDataFromExcels2[i].UTMy;
                    worksheet.Cells["I" + (i + 2).ToString()].Value = _UnityUTMDataFromExcels2[i].UTMz;
                    worksheet.Cells["J" + (i + 2).ToString()].Value = _UnityUTMDataFromExcels2[i].Posx;
                    worksheet.Cells["K" + (i + 2).ToString()].Value = _UnityUTMDataFromExcels2[i].Posy;
                    worksheet.Cells["L" + (i + 2).ToString()].Value = _UnityUTMDataFromExcels2[i].Posz;

                }
                ////添加一行数据
                //worksheet.Cells["A2"].Value = 12001;
                //worksheet.Cells["B2"].Value = "Nails";
                //worksheet.Cells["C2"].Value = 37;
                //worksheet.Cells["D2"].Value = 3.99;
                ////添加一行数据
                //worksheet.Cells["A3"].Value = 12002;
                //worksheet.Cells["B3"].Value = "Hammer";
                //worksheet.Cells["C3"].Value = 5;
                //worksheet.Cells["D3"].Value = 12.10;
                ////添加一行数据
                //worksheet.Cells["A4"].Value = 12003;
                //worksheet.Cells["B4"].Value = "Saw";
                //worksheet.Cells["C4"].Value = 12;
                //worksheet.Cells["D4"].Value = 15.37;

                //保存excel
                package.Save();
            }
        }
    }
}
[System.Serializable]
public class UnityUTMDataFromExcel
{
    public string ID, Area_box_ID, Floor;
    public string Name, Tag, TenantID;
    public string UTMx, UTMy, UTMz, Posx, Posy, Posz, Rotx, Roty, Rotz, Scalex, Scaley, Scalez;

}
