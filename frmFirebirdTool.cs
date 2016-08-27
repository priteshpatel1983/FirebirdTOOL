using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using FirebirdSql.Data.FirebirdClient;
using FireBird.SQLHelper;
using FirebirdSql.Data.Services;

using MySql.Data.MySqlClient;
using System.Data.SqlClient;
//using POSPayment;


namespace FirebirdTool
{
    public partial class frmFirebirdTool : Form
    {
        public  DataTable dt;
        POSDataLib dlib = new POSDataLib();
        
        string strServerConn = "";      
        public frmFirebirdTool()
        {
            
            /* Step for Batch Close
             *  all change as per only Gateway method
             *  you did not any change for Manual just select Register and batch and  click on close Button
             * If you want to run this batch from different register then you have to change reigister as  Batch Close Register
             if you from Server 
             
             */
            InitializeComponent();
            dateTimePicker2.Value = DateTime.Now.AddMonths(-24);
            dateTimePicker1.Value = DateTime.Now.AddMonths(-36);
            txtBackupFolder.Text = "C:\\POS2020Log";
            lblStatus.Text = "";
            string strPad="000000000000";
            MessageBox.Show(strPad.TrimStart('0'));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select file";
           // fdlg.InitialDirectory = @"d:\pos";
            fdlg.FileName = txtFileName.Text;
            //fdlg.Filter = "Text and CSV Files(*.csv)|*.txt;*.csv|Text Files(*.txt)|*.txt|CSV Files(*.csv)|*.csv|All files(*.*)|*.*";
            fdlg.Filter = "Text and CHP Files(*.chp)|*.txt;*.chp|Text Files(*.txt)|*.txt|CHP Files(*.chp)|*.chp|All files(*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = fdlg.FileName;               

            }

            try
            {                

                if (CheckValue())
                {
                    strServerConn = "User=SYSDBA;Password=masterkey;Database=" + txtFileName.Text + ";DataSource=" + txtServerName.Text + ";Pooling=false";
                    FbConnection conn = new FbConnection(strServerConn);
                    conn.Open();
                    conn.Close();                                     
                    

                }
            }
            catch (Exception ee)
            {
                lblStatus.Text = ee.Message.ToString();
                lblStatus.Refresh();
            }

        }

        private bool CheckValue()
        {
            bool result = true;
            lblStatus.Text = "";
            lblStatus.Refresh();
            if ((txtServerName.Text.Trim().Length) == 0)
            {
                result = false;
                lblStatus.Text = "Enter the ServerName";
                lblStatus.Refresh();
                txtServerName.Text = "";
                txtServerName.Focus();
                return result;
            }
            if ((txtFileName.Text.Trim().Length) == 0)
            {
                result = false;
                lblStatus.Text = "Enter the FileName";
                lblStatus.Refresh();
                txtFileName.Text = "";
                txtFileName.Focus();
                return result;
            }

            return result;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckValue())
                {
                    strServerConn = "User=SYSDBA;Password=masterkey;Database=" + txtFileName.Text + ";DataSource=" + txtServerName.Text + ";Pooling=false";
                    if (txtStrQuery.Text.Trim().Length == 0)
                    {
                        lblStatus.Text = "Enter the Query";
                        lblStatus.Refresh();
                        txtStrQuery.Text = "";
                        txtStrQuery.Focus();
                        return;
                    }
                    if (txtStrQuery.Text.ToLower().StartsWith("select"))
                    {
                        DataTable dt = new DataTable();
                        dt = dlib.ExecuteDatTable(strServerConn,txtStrQuery.Text.Trim().ToString());
                        dataGridView1.DataSource = dt.DefaultView;

                    }
                    else
                    {

                        if (txtStrQuery.Text.ToLower().StartsWith("create") || txtStrQuery.Text.ToLower().StartsWith("alter") || txtStrQuery.Text.ToLower().StartsWith("drop"))
                        {
                          
                            dlib.ExecuteNonQuery(strServerConn, txtStrQuery.Text.Trim().ToString());
                            lblStatus.Text = "Successfully CMD  ";
                            lblStatus.Refresh(); 
                        }
                        else
                        {
                            string[] str = txtStrQuery.Text.Trim().Split(';');
                            for (int k = 0; k < str.Length; k++)
                            {
                                if (str[k].ToString().Trim().Length > 0)
                                    dlib.ExecuteNonQuery(strServerConn, str[k].ToString());
                            }
                            lblStatus.Text = "Run Successfully  ";
                            lblStatus.Refresh();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.Message.ToString();
                lblStatus.Refresh();
            }
            
        }

        private void btnOnline_Click(object sender, EventArgs e)
        {

            try
            {
                
                  
                    if (txtStrQuery.Text.Trim().Length == 0)
                    {
                        lblStatus.Text = "Enter the Query";
                        lblStatus.Refresh();
                        txtStrQuery.Text = "";
                        txtStrQuery.Focus();
                        return;
                    }
                    if (txtStrQuery.Text.ToLower().StartsWith("select"))
                    {
                        DataTable dt = new DataTable();
                        dt = dlib.ExecuteDatTable(strServerConn, txtStrQuery.Text.Trim().ToString());
                        dataGridView1.DataSource = dt.DefaultView;
                                     
                        dt = new DataTable();
                        MySqlDataAdapter data = new MySqlDataAdapter(txtStrQuery.Text.Trim(), strServerConn);
                        data.SelectCommand.CommandTimeout = 0;
                        data.Fill(dt);
                       
                        dataGridView1.DataSource = dt;
                        lblStatus.Text = dt.Rows.Count.ToString() ;
                        lblStatus.Refresh();

                    }
                    else
                    {

                        if (txtStrQuery.Text.ToLower().StartsWith("create") || txtStrQuery.Text.ToLower().StartsWith("alter") || txtStrQuery.Text.ToLower().StartsWith("drop"))
                        {
                            MySqlHelper.ExecuteNonQuery(strServerConn, txtStrQuery.Text.Trim().ToString());
                                lblStatus.Text = "Run Successfully";
                                lblStatus.Refresh();
                        }
                        else
                        {
                            string[] str = txtStrQuery.Text.Trim().Split(';');
                            for (int k = 0; k < str.Length; k++)
                            {                           
                                if(str[k].ToString().Trim().Length>0)
                                    MySqlHelper.ExecuteNonQuery(strServerConn, str[k].ToString());
                            }
                            lblStatus.Text = "Run Successfully";
                            lblStatus.Refresh();
                        }
                        
                    }
                
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.Message.ToString();
                lblStatus.Refresh();
            }
        }

        private void btnExtractData_Click(object sender, EventArgs e)
        {
            try
            {
                string strTableName="";
                string strFullQuery = "";
                string strFileName = Application.StartupPath;
                string strLine = "";
                StringBuilder strDataLine = new StringBuilder();
                string strCat = string.Empty;
                if (txtStrQuery.Text.ToLower().StartsWith("select"))
                {
                    DataTable dt = new DataTable();
                    strServerConn = "User=SYSDBA;Password=masterkey;Database=" + txtFileName.Text + ";DataSource=" + txtServerName.Text + ";Pooling=false";
                    string strQ = "";
                    strQ = txtStrQuery.Text.ToString();
                    string[] strMultiQuery = strQ.Split(';');
                    for (int k = 0; k < strMultiQuery.Length; k++)
                    {
                     strQ = strMultiQuery[k];

                    dt = dlib.ExecuteDatTable(strServerConn, strQ.Trim().ToString());
                    
                    if (dt.Rows.Count > 0)
                    {
                        //Get TableName
                        dataGridView1.DataSource = dt.DefaultView;
                              strTableName = (strQ.Substring(strQ.ToLower().IndexOf("from") + 4)).Trim(' ');
                              if (chkDeleteQuery.Checked)
                              {
                                  strLine = "DELETE FROM " + strTableName + ";\r\n";
                                  strFullQuery += strLine;
                              }
                            if (strTableName.IndexOf(' ') > 0)
                                strTableName = strTableName.Substring(0, strTableName.IndexOf(' '));
                           
                            string strColume = "";
                            string strColumnData = "";
                            if (strMultiQuery.Length > 1)
                            {
                                strFileName = Application.StartupPath;
                                strFileName = strFileName + "\\FULL"+".sql";
                                if ((File.Exists(strFileName)) && k==0)
                                    File.Delete(strFileName);
                            }
                            else
                            {
                                strFileName = Application.StartupPath;
                                strFileName = strFileName + "\\" + strTableName + ".sql";
                                if (File.Exists(strFileName))
                                    File.Delete(strFileName);
                            }
                            int i = 0;
                            int col = 0;
                           // StringBuilder strCol = new StringBuilder();
                          //  StringBuilder strColData = new StringBuilder();
                           
                            foreach (DataRow dr in dt.Rows)
                            {

                                if (i == 0)
                                {
                                    for (col = 0; col < dt.Columns.Count; col++)
                                    {
                                        strColume += dt.Columns[col].ColumnName + ",";
                                        //strCol.Append(dt.Columns[col].ColumnName);
                                        //strCol.Append(",");

                                        i++;
                                    }
                                }
                                strColumnData = "";
                                for (col = 0; col < dt.Columns.Count; col++)
                                {
                                    if (dt.Columns[col].DataType.Name.ToUpper().StartsWith("S"))
                                    {
                                        strColumnData += "'" + dr[col].ToString().Replace("'", "") + "',";
                                       // strColData.Append("'" + dr[col].ToString().Replace("'", "") + "'");
                                       // strColData.Append(",");
                                    }
                                    else if (dt.Columns[col].DataType.Name.ToUpper().StartsWith("DATE"))
                                    {
                                        strColumnData += retunNull(dr[col].ToString()) + ",";
                                        //strColData.Append(retunNull(dr[col].ToString()));
                                        //strColData.Append(",");
                                    }
                                    else if (dt.Columns[col].DataType.Name.ToUpper().StartsWith("TIME"))
                                    {
                                        strColumnData += retunTimeNull(dr[col].ToString()) + ",";
                                       // strColData.Append(retunTimeNull(dr[col].ToString()));
                                        //strColData.Append(",");
                                    }
                                    else if (dt.Columns[col].DataType.Name.ToUpper().StartsWith("BYTE"))
                                    {
                                        if (dr[col].ToString().Length > 0)
                                        {
                                            //strColumnData += retunByteNull(dr[col].ToString()) + ",";
                                            strColumnData += retunByteNull(Convert.ToBase64String((Byte[])dr[col]).ToString()) + ",";
                                            //strColData.Append(retunByteNull(Convert.ToBase64String((Byte[])dr[col]).ToString()));
                                           // strColData.Append(",");
                                        }
                                        else
                                            strColumnData +=  "NULL,";

                                    }
                                    else
                                    {
                                        strColumnData += retunZero(dr[col].ToString()) + ",";
                                        //strColData.Append(retunZero(dr[col].ToString()));
                                        //strColData.Append(",");
                                    }                                    

                                }

                                
                                
                                   strLine = "insert into " + strTableName + "(" + strColume.TrimEnd(',') + " ) " + "Values (" + strColumnData.TrimEnd(',') + ");\r\n";
                                //strLine = "insert into " + strTableName + "(" + strCol.ToString().TrimEnd(',') + " ) " + "Values (" + strColData.ToString().TrimEnd(',') + ");\r\n";
                                    
                                    strFullQuery += strLine;

                              //  strDataLine.Append(strLine);

                               
                            }
                            
                        }//For Loop
                                               
                    }//Ending loop for Multiple Query
                    if (strFullQuery.Length > 0)
                    {
                        TextWriter txtWrite = new StreamWriter(strFileName, true);
                        txtWrite.WriteLine(strFullQuery.ToString());
                        txtWrite.Close();
                    }
                    lblStatus.Text = "Completed";
                    lblStatus.Refresh();

                    txtStrQuery.Text = strFullQuery.ToString(); 
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.Message.ToString();
                lblStatus.Refresh();
            }
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            strServerConn = "User=SYSDBA;Password=masterkey;Database=" + txtFileName.Text + ";DataSource=" + txtServerName.Text + ";Pooling=false";
            LoadBatch();
        }

        private void btnPurgeData_Click(object sender, EventArgs e)
        {
            try
            {
                string strTableName = "";
                string strFullQuery = "";
                string strFullDELQuery="";
                string strFileName = txtBackupFolder.Text + "\\BKP_" + dateTimePicker2.Value.ToString("MM-dd-yyyy") + "_" + dateTimePicker1.Value.ToString("MM-dd-yyyy") + ".sql"; ;
                string strLine = "";

                if (chkBackPurge.Checked)
                {
                    if (txtBackupFolder.Text.Trim().Length == 0)
                    {
                        txtBackupFolder.Focus();
                        MessageBox.Show("SELECT BACKUP FOLDER");
                        return;
                    }
                }
                List<string>strList  = new List<string>();
                List<string> strListDelete = new List<string>();
                strList.Add("select * from trn_batch where CAST(DBATCHSTARTTIME as DATE ) between");
                strList.Add("select * from TRN_CUSTOMERPAY where CAST(DTRANDATE as DATE ) between");
                strList.Add("select * from TRN_DAILYSALES where CAST(DDATE as DATE ) between");
                strList.Add("select * from TRN_SALEPRICE where CAST(DSALECREATEDATE as DATE ) between ");
                strList.Add("select * from TRN_SALESCUSTOMER where ISALESID IN(select ISALESID from  TRN_SALES where CAST(DTRANDATE as DATE ) between ");
                strList.Add("select * from TRN_SALESTENDER where ISALESID IN(select ISALESID from  TRN_SALES where CAST(DTRANDATE as DATE ) between ");
                strList.Add("select * from TRN_SALESDETAIL where ISALESID IN(select ISALESID from  TRN_SALES where CAST(DTRANDATE as DATE ) between ");
                strList.Add("select * from TRN_SALES where CAST(DTRANDATE as DATE ) between ");   
                /*
                strList.Add("select * from TRN_INSTANTACTIVEBOOK where CAST(DACTIVEDATE as DATE ) between");
                strList.Add("select * from TRN_INSTANTCLOSEDAYDETAIL where ibatchid in (select ibatchid from TRN_INSTANTCLOSEDAY where CAST(DCLOSEDATE as DATE) between ");
                strList.Add("select * from TRN_INSTANTCLOSEDAY where CAST(DCLOSEDATE as DATE) between ");                
                strList.Add("select * from TRN_LEGER where CAST(DLEGERDATE as DATE ) between ");               
                strList.Add("select * from TRN_ORDERSERVING where CAST(DORDERDATE as DATE ) between ");
                strList.Add("select * from TRN_PAIDOUTDETAIL where ipaidoutid IN(select iPaidoutid from TRN_PAIDOUT where CAST(DDATE as DATE ) between ");
                strList.Add("select * from TRN_PAIDOUT where CAST(DDATE as DATE ) between ");
                strList.Add("select * from TRN_PHYSICALINVENTORYDETAIL where ipiid IN(select ipiid from TRN_PHYSICALINVENTORY  where CAST(DCREATEDATE as DATE ) between ");
                strList.Add("select * from TRN_PHYSICALINVENTORY where CAST(DCREATEDATE as DATE ) between ");
                strList.Add("select * from TRN_PICKUPCASH where CAST(DDATETIME as DATE ) between ");
                strList.Add("select * from TRN_PURCHASEORDERDETAIL where ipoid IN(select IPOID from TRN_PURCHASEORDER  where CAST(DCREATEDATE as DATE ) between ");
                strList.Add("select * from TRN_PURCHASEORDER where CAST(DCREATEDATE as DATE ) between ");
                strList.Add("select * from TRN_PUSHNOTIFICATION where CAST(DDATE as DATE ) between ");
                strList.Add("select * from TRN_QUOTATIONDETAIL where IQUOTATIONID IN(select IQUOTATIONID from TRN_QUOTATION  where CAST(DTRANDATE as DATE ) between ");
                strList.Add("select * from TRN_QUOTATION where CAST(DTRANDATE as DATE ) between ");
                strList.Add("select * from TRN_SALEPRICEDETAIL where ISALEPRICEID IN(select ISALEPRICEID from TRN_SALEPRICE where CAST(DSALECREATEDATE as DATE ) between ");
                strList.Add("select * from TRN_SALEPRICE where CAST(DSALECREATEDATE as DATE ) between ");
                strList.Add("select * from TRN_SALESCUSTOMER where ISALESID IN(select ISALESID from  TRN_SALES where CAST(DTRANDATE as DATE ) between ");
                strList.Add("select * from TRN_SALESTENDER where ISALESID IN(select ISALESID from  TRN_SALES where CAST(DTRANDATE as DATE ) between ");
                strList.Add("select * from TRN_SALESDETAIL where ISALESID IN(select ISALESID from  TRN_SALES where CAST(DTRANDATE as DATE ) between ");
                strList.Add("select * from TRN_SALES where CAST(DTRANDATE as DATE ) between ");               
                strList.Add("select * from TRN_VOIDTRAN where CAST(DTRANDATE as DATE ) between ");
                strList.Add("select * from TRN_WAREHOUSEINVOICE where CAST(DINVOICEDATE as DATE ) between ");
                strList.Add("select * from TRN_WAREHOUSEITEMS where INVOICEID IN(select VINVNUM from  TRN_WAREHOUSEINVOICE where CAST(DINVOICEDATE as DATE ) between ");
                strList.Add("select * from TRN_WAREHOUSEINVOICE where CAST(DINVOICEDATE as DATE ) between ");

                strListDelete.Add("select * from TRN_USERLOGDETAIL where IUSERLOGID IN(select IUSERLOGID from  TRN_USERLOG where CAST(DLOGINDATETIME as DATE ) between ");
                strListDelete.Add("select * from TRN_USERLOG where CAST(DLOGINDATETIME as DATE ) between ");
                 strListDelete.Add("select * from TRN_MPSBATCHCLOSE where batchno in (select ibatchid from trn_batch where CAST(DBATCHSTARTTIME as DATE ) between ");
                 strListDelete.Add("select * from TRN_MPSBATCHSMRY where batchno in (select ibatchid from trn_batch where CAST(DBATCHSTARTTIME as DATE ) between ");
                 strListDelete.Add("select * from TRN_MPSREQUEST where CAST(DTRANDATE as DATE ) between ");
                 strListDelete.Add("select * from TRN_MPSTENDER where CAST(DTRANDATE as DATE ) between ");
                strListDelete.Add("select * from TRN_INVENTORY where CAST(DDATE as DATE ) between ");
                strListDelete.Add("select * from TRN_ITEMPRICECOSTHISTORY where CAST(DHISTORYDATE as DATE ) between ");
                 */
                Cursor.Current = Cursors.WaitCursor;
                
                if (dateTimePicker2.Value.Date < dateTimePicker1.Value.Date)
                {
                    dateTimePicker2.Focus();
                    MessageBox.Show("To date can not be less than from date"); 
                    return;
                }
               /* if (DateTime.Now.AddMonths(-24) < dateTimePicker2.Value)
                {
                    MessageBox.Show("You can not delete last TWO year data");
                    return;
                }*/
                strFullDELQuery = "";
               // if (txtStrQuery.Text.ToLower().StartsWith("select"))
                //{
                    DataTable dt = new DataTable();
                    strServerConn = "User=SYSDBA;Password=masterkey;Database=" + txtFileName.Text + ";DataSource=" + txtServerName.Text + ";Pooling=false";
                    string strQ = "";
                    strQ = txtStrQuery.Text.ToString();
                    string strDeleteQuery = "";
                    //string[] strMultiQuery = strQ.Split(';');
                    writeLogMessage("Start Purge Data");
                    lblStatus.Text = "Runnig";
                    lblStatus.Update();
                    for (int k = 0; k < strListDelete.Count; k++)
                    {
                        strQ = strListDelete[k].ToString() + " '" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "' AND '" + dateTimePicker2.Value.ToString("MM/dd/yyyy") + "'";
                        if (strQ.LastIndexOf("select") > 0)
                            strQ = strQ + " )";
                        strDeleteQuery = strQ.Replace("select *", "DELETE");
                       // dlib.ExecuteNonQuery(strServerConn, strDeleteQuery);
                        strFullDELQuery += strDeleteQuery + ";\r\n"; 
                    }
                    strDeleteQuery = "";
                    for (int k = 0; k < strList.Count; k++)
                    {
                        strQ = strList[k].ToString() + " '" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "' AND '" + dateTimePicker2.Value.ToString("MM/dd/yyyy")+"'";
                        if (strQ.LastIndexOf("select") > 0)
                            strQ = strQ + " )";
                        strDeleteQuery = strQ.Replace("select *", "DELETE") + ";\r\n";  ;

                        if (chkBackPurge.Checked)
                        {
                            if (k == 0)
                                writeLogMessage("Taking Backup Data");
                            dt = dlib.ExecuteDatTable(strServerConn, strQ.Trim().ToString());
                            
                            if (dt.Rows.Count > 0)
                            {                                
                                strTableName = (strQ.Substring(strQ.ToLower().IndexOf("from") + 4)).Trim(' ');
                              
                                if (strTableName.IndexOf(' ') > 0)
                                    strTableName = strTableName.Substring(0, strTableName.IndexOf(' '));
                                writeLogMessage("Geting Datafrom this table: " + strTableName);
                               
                                string strColumnData = "";                                
                                //int i = 0;
                                int col = 0;
                                string strColume = string.Join(","
    , dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName));
                               
                                foreach (DataRow dr in dt.Rows)
                                {
                                    strColumnData = "";
                                     //strColumnData = string.Join(",", dr.ItemArray);
                                   
                                    for (col = 0; col < dt.Columns.Count; col++)
                                    {

                                        if (dt.Columns[col].DataType.Name.ToUpper().StartsWith("S"))
                                        {
                                            strColumnData += "'" + dr[col].ToString().Replace("'", "") + "',";
                                        }
                                        else if (dt.Columns[col].DataType.Name.ToUpper().StartsWith("DATE"))
                                        {
                                            strColumnData += retunNull(dr[col].ToString()) + ",";
                                        }
                                        else if (dt.Columns[col].DataType.Name.ToUpper().StartsWith("DATETIME"))
                                        {
                                            strColumnData += retunDateTimeNull(dr[col].ToString()) + ",";
                                        }
                                        else if (dt.Columns[col].DataType.Name.ToUpper().StartsWith("TIME"))
                                        {
                                            strColumnData += retunTimeNull(dr[col].ToString()) + ",";
                                        }
                                        else if (dt.Columns[col].DataType.Name.ToUpper().StartsWith("BYTE"))
                                        {
                                            if (dr[col].ToString().Length > 0)
                                                //strColumnData += retunByteNull(dr[col].ToString()) + ",";
                                               strColumnData += retunByteNull(Convert.ToBase64String((Byte[])dr[col]).ToString()) + ",";
                                            else
                                                strColumnData += "NULL,";

                                        }
                                        else
                                        {
                                            strColumnData += retunZero(dr[col].ToString()) + ",";
                                        }
                                        
                                    }
                                    
                                    strLine = string.Format("INSERT INTO {0} ({1}) VALUES ({2});", strTableName, strColume, strColumnData.TrimEnd(',')) + "\r\n";
                                    
                                    strFullQuery += strLine;
                                   // RowCount = RowCount + 1;
                                    //lblStatus.Text = RowCount.ToString();
                                   // lblStatus.Update();
                                }
                               /* foreach (DataRow dr in dt.Rows)
                                {

                                    if (i == 0)
                                    {
                                        for (col = 0; col < dt.Columns.Count; col++)
                                        {
                                            strColume += dt.Columns[col].ColumnName + ",";
                                            i++;
                                        }
                                    }
                                    strColumnData = "";
                                    for (col = 0; col < dt.Columns.Count; col++)
                                    {
                                        
                                        if (dt.Columns[col].DataType.Name.ToUpper().StartsWith("S"))
                                        {
                                            strColumnData += "'" + dr[col].ToString().Replace("'", "") + "',";
                                        }
                                        else if (dt.Columns[col].DataType.Name.ToUpper().StartsWith("DATE"))
                                        {
                                            strColumnData += retunNull(dr[col].ToString()) + ",";
                                        }
                                        else if (dt.Columns[col].DataType.Name.ToUpper().StartsWith("DATETIME"))
                                        {
                                            strColumnData += retunDateTimeNull(dr[col].ToString()) + ",";
                                        }
                                        else if (dt.Columns[col].DataType.Name.ToUpper().StartsWith("TIME"))
                                        {
                                            strColumnData += retunTimeNull(dr[col].ToString()) + ",";
                                        }
                                        else
                                        {
                                            strColumnData += retunZero(dr[col].ToString()) + ",";
                                        }    

                                    }
                                    strLine = "insert into " + strTableName + "(" + strColume.TrimEnd(',') + " ) " + "Values (" + strColumnData.TrimEnd(',') + ");\r\n";
                                    
                                    strFullQuery += strLine;
                                    lblStatus.Text = strLine;
                                    lblStatus.Refresh();
                                }//Column query*/
                                
                            }//rowCount
                        }//Backup Condition
                        //dlib.ExecuteNonQuery(strServerConn, strDeleteQuery);
                        strFullDELQuery += strDeleteQuery ;
                    }//Ending loop for Multiple Query
                    if ((strFullQuery.Length > 0 ) && chkBackPurge.Checked)
                    {
                        writeLogMessage("End Backup Data");
                        if ((File.Exists(strFileName)))
                            File.Delete(strFileName);
                        TextWriter txtWrite = new StreamWriter(strFileName, true);
                        txtWrite.WriteLine(strFullQuery); 
                        txtWrite.Close();
                    }
                    lblStatus.Text = "Completed";
                    lblStatus.Refresh();
                    Cursor.Current = Cursors.Arrow;
                    writeLogMessage("Finish Purge Data");
                   
                    
                    //txtStrQuery.Text = strFullQuery;
                //}
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.Message.ToString();
                lblStatus.Refresh();
            }
        }
        private void CloseBatch(string ibatchid)
        {
            //here clos batch and it will be insert automaticlly new batch
            try
            {
                string strSelect = string.Empty;
                DataTable dtBatch = new DataTable();


                strSelect = "SELECT * FROM trn_Batch where ibatchid='" + ibatchid + "'";
                dtBatch = dlib.ExecuteDatTable(strServerConn, strSelect);
                if (dtBatch.Rows.Count > 0)
                {
                    //int iBatchid = 0;
                    double nnettotal = 0;
                    double nnetpickup = 0;
                    double nnetpaidout = 0;
                    double nnetopening = 0;
                    double nnetaddcash = 0;
                    double nnetclose = 0;
                    double nTotalNonTaxable = 0;
                    double nTotalTaxable = 0;
                    double nTotalSales = 0;
                    double nTotalTax = 0;
                    double nTotalCreditSales = 0;
                    double nTotalDebitSales = 0;
                    double nTotalEBTSales = 0;
                    double nTotalCashSales = 0;
                    double nTotalGiftSales = 0;
                    double nTotalCheckSales = 0;
                    double nTotalReturns = 0;
                    double nTotalDiscount = 0;
                    double nTotalonacct = 0;
                    string strDateTime = string.Empty;
                    //strDateTime = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");
                    DataRow drBatch;
                    drBatch = dtBatch.Rows[0];


                    nnetopening = Convert.ToDouble(drBatch["NOPENINGBALANCE"].ToString());

                    //DateTime dtSTime = new DateTime();
                    // dtSTime = Convert.ToDateTime(drBatch["dbatchstarttime"].ToString());
                    DataTable dtTrn = new DataTable();
                    strSelect = "select Case  when sum(nnettotal) is null then 0 ELSE sum(nnettotal) end As nnetotal,vtrntype from trn_Sales " +
                                " where  vtrntype  in ('Transaction','Cash pickup','Add Cash') and  ionaccount != 1  and ibatchid= '" + ibatchid + "'" +
                                " group by vtrntype";



                    dtTrn = dlib.ExecuteDatTable(strServerConn, strSelect);
                    if (dtTrn.Rows.Count > 0)
                    {
                        foreach (DataRow drT in dtTrn.Rows)
                        {
                            switch (drT["vtrntype"].ToString())
                            {
                                case "Transaction":
                                    nnettotal = Convert.ToDouble(drT["nnetotal"].ToString());
                                    break;
                                case "Cash pickup":
                                    nnetpickup = Convert.ToDouble(drT["nnetotal"].ToString());
                                    break;
                                case "Add Cash":
                                    nnetaddcash = Convert.ToDouble(drT["nnetotal"].ToString());
                                    break;
                            }


                        }
                    }
                    strSelect = "select  Case  when sum(namount) is null then 0 ELSE sum(namount) end As nnetotal " +
                                "from trn_paidout a,trn_paidoutdetail b where  a.ipaidouttrnid = b.ipaidouttrnid " +
                                " and  a.ibatchid='" + ibatchid + "' ";



                    dtTrn = new DataTable();
                    dtTrn = dlib.ExecuteDatTable(strServerConn,strSelect);
                    if (dtTrn.Rows.Count > 0)
                    {
                        nnetpaidout = Convert.ToDouble(dtTrn.Rows[0]["nnetotal"].ToString());
                    }
                    //GetIDForUpdatBatch
                    //CalTotal Cash
                    nnetclose = (nnetopening + nnettotal + nnetaddcash) - (nnetpickup + nnetpaidout);
                    //CalculteTotalTA
                    strSelect = "SELECT  Case  when sum(NNONTAXABLETOTAL) is null then 0 ELSE sum(NNONTAXABLETOTAL) end as NNONTAXABLETOTAL,Case  when sum(NTAXABLETOTAL) is null then 0 ELSE sum(NTAXABLETOTAL) end As NTAXABLETOTAL, "
                                 + "Case  when sum(NTAXTOTAL) is null then 0 ELSE sum(NTAXTOTAL) end As NTAXTOTAL ,"
                                 + " Case  when sum(NSUBTOTAL) is null then 0 ELSE sum(NSUBTOTAL) end As NSUBTOTAL,Case  when sum(NDISCOUNTAMT) is null then 0 ELSE sum(NDISCOUNTAMT) end As NDISCOUNTAMT from TRN_Sales  "
                                 + "WHERE ionaccount != 1 and vtrntype  in ('Transaction')  and ibatchid= '" + ibatchid + "'";



                    dtTrn = new DataTable();
                    dtTrn = dlib.ExecuteDatTable(strServerConn,strSelect);
                    if (dtTrn.Rows.Count > 0)
                    {

                        nTotalNonTaxable = Convert.ToDouble(dtTrn.Rows[0]["NNONTAXABLETOTAL"].ToString());
                        nTotalTaxable = Convert.ToDouble(dtTrn.Rows[0]["NTAXABLETOTAL"].ToString());
                        nTotalSales = Convert.ToDouble(dtTrn.Rows[0]["NSUBTOTAL"].ToString());
                        nTotalTax = Convert.ToDouble(dtTrn.Rows[0]["NTAXTOTAL"].ToString());
                        nTotalDiscount = Convert.ToDouble(dtTrn.Rows[0]["NDISCOUNTAMT"].ToString());
                    }

                    strSelect = "SELECT vtendertag,Case  when sum(a.namount) is null then 0 ELSE sum(a.namount) end as namount"
                                + " FROM TRN_SALESTENDER a,trn_sales b,MST_Tentertype c"
                                + "  WHERE  b.ibatchid= '" + ibatchid + "' and a.isalesid = b.isalesid and b.vtrntype  in ('Transaction') and a.itenerid = c.itenderid group by vtendertag ";



                    dtTrn = new DataTable();
                    dtTrn = dlib.ExecuteDatTable(strServerConn, strSelect);
                    if (dtTrn.Rows.Count > 0)
                    {
                        foreach (DataRow drT in dtTrn.Rows)
                        {
                            switch (drT["vtendertag"].ToString())
                            {
                                case "Credit":
                                    nTotalCreditSales = Convert.ToDouble(drT["namount"].ToString());
                                    break;
                                case "Debit":
                                    nTotalDebitSales = Convert.ToDouble(drT["namount"].ToString());
                                    break;
                                case "Gift":
                                    nTotalGiftSales = Convert.ToDouble(drT["namount"].ToString());
                                    break;
                                case "Ebt":
                                    nTotalEBTSales = Convert.ToDouble(drT["namount"].ToString());
                                    break;
                                case "OnAcct":
                                    nTotalonacct = Convert.ToDouble(drT["namount"].ToString());
                                    break;
                            }
                        }

                    }

                    strSelect = "SELECT vtendername,Case  when sum(a.namount) is null then 0 ELSE sum(a.namount) end as namount"
                                + " FROM TRN_SALESTENDER a,trn_sales b"
                                + "  WHERE a.isalesid = b.isalesid and  b.vtrntype  in ('Transaction')  and vtendername in ('Cash','Check','ReturnItem') and    b.ibatchid= '" + ibatchid + "'  group by vtendername ";



                    dtTrn = new DataTable();
                    dtTrn = dlib.ExecuteDatTable(strServerConn, strSelect);
                    if (dtTrn.Rows.Count > 0)
                    {
                        foreach (DataRow drT in dtTrn.Rows)
                        {
                            switch (drT["vtendername"].ToString())
                            {
                                case "Cash":
                                    nTotalCashSales = Convert.ToDouble(drT["namount"].ToString());
                                    break;
                                case "Check":
                                    nTotalCheckSales = Convert.ToDouble(drT["namount"].ToString());
                                    break;
                                case "ReturnItem":
                                    nTotalReturns = Convert.ToDouble(drT["namount"].ToString());
                                    break;
                            }
                        }
                    }
                    //Minus Cash From Return
                    //CalTotal Cash

                    nnettotal = nTotalCashSales + nTotalCheckSales + nTotalCreditSales + nTotalDebitSales + nTotalGiftSales + nTotalEBTSales + nTotalonacct;
                    nnetclose = (nnetopening + nnettotal + nnetaddcash) - (Math.Abs(nTotalReturns) - nnetpickup + nnetpaidout);
                    nTotalCashSales = nTotalCashSales + Math.Abs(nTotalReturns);
                    strSelect = "UPDATE  trn_batch set ionupload='0',estatus ='Close',nnetsales=" + nnettotal + ",nnetcashpickup=" + nnetpickup + ",nnetpaidout=" + nnetpaidout + " ,NCLOSINGBALANCE='" + nnetclose + "',NUSERCLOSINGBALANCE='" + nnetclose + "',NNETADDCASH='" + nnetaddcash + "' " +
                        " ,nTotalNonTaxable=" + nTotalNonTaxable + ",nTotalTaxable=" + nTotalTaxable + ",nTotalSales=" + nTotalSales + ",NTotalTax=" + nTotalTax + ",nTotalDiscount=" + nTotalDiscount + "" +
                        ",nTotalCreditSales=" + nTotalCreditSales + ",nTotalGiftSales=" + nTotalGiftSales + ",nTotalDebitSales=" + nTotalDebitSales + ",nTotalEBTSales=" + nTotalEBTSales + "" +
                        ",nTotalCashSales=" + nTotalCashSales + ",nTotalCheckSales=" + nTotalCheckSales + ",nTotalReturns=" + nTotalReturns + ""
                        + " where CAST(ibatchid as varchar(30))='" + drBatch["iBatchid"] + "' ";

                   dlib.ExecuteNonQuery(strServerConn, strSelect);


                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error while closing batch. Please contact your Administrator. " + ex.Message);
            }

        }
        protected void CreditCardTransaction()
        {

            string strQuery = string.Empty;
            string strCreditID = string.Empty;
            string strLocConn = "User=SYSDBA;Password=masterkey;Database=D:\\pos\\FirebirdTool\\bin\\Local.chp;DataSource=localhost;Pooling=false"; ;
            string strSerConn = "User=SYSDBA;Password=masterkey;Database=C:\\SonicPro\\SystemCache\\SYSPRODLL.CHP;DataSource=localhost;Pooling=false";
            string strSalesTrnID = "0";
            try
            {
               

                DataSet objDs;
                DataSet objSalesTranID;
                char[] specialcahe = { '\0' };
                //Get all newly inserted transac;tin data

                //Get the last 10 sales ID
                strQuery = "SELECT ITRANID FROM TRN_MPSTENDER " +
                            " where (vTransfer='No' or vTransfer is null) and ITRANID is not null rows 10";
                objSalesTranID = SqlHelper.ExecuteDataset(strLocConn, CommandType.Text, strQuery);
                if (objSalesTranID.Tables.Count > 0)
                {
                    DataTable dtSales = objSalesTranID.Tables[0];

                    foreach (DataRow dr in dtSales.Rows)
                    {
                        strSalesTrnID += dr["ITRANID"].ToString() + ",";
                    }

                    strSalesTrnID = strSalesTrnID.TrimEnd(',');
                }
                else
                { strSalesTrnID = "0"; }

                strQuery = "SELECT IMPSTENDERID,VTOKEN,DTRANDATE,DTRANTIME,VCMDSTATUS,VTEXTRESPONSE,VPROCESSDATA,VAUTHCODE,NPURCHASEAMOUNT,NAUTHAMOUNT,VBATCHNO,NBATCHAMOUNT,VCARDTYPE,VCARDHOLDERNAME,VRETURNCODE,VSIGNATUREDATA,VTENDERTYPE,VVOUCHERNO,VMEMO,VTRANCODE,VCARDUSAGE,VAVSRESULT,VCAPTURESTATUS,VACQREFDATA,VOPERATORID,VPREPAIDACCOUNT,VUSERID,VINVNUMBER,VREFNUMBER,XMLRESPONSE,ITRANID FROM TRN_MPSTENDER  where ITRANID in (" + strSalesTrnID + ")";
                objDs = SqlHelper.ExecuteDataset(strLocConn, CommandType.Text, strQuery);


                //Insert MAster Record
                if (objDs.Tables.Count > 0)
                {
                    DataTable dtSales = objDs.Tables[0];

                    foreach (DataRow dr in dtSales.Rows)
                    {
                        //Delete Master Record
                        // SqlHelper.ExecuteNonQuery(strSerConn, CommandType.Text, "execute procedure SP_DELETESALESTRAN " + dr["ISALESID"].ToString());
                        //Delete Fro if Exist
                        strQuery = "delete from TRN_MPSTENDER where ITRANID=" + dr["ITRANID"].ToString();
                        SqlHelper.ExecuteNonQuery(strSerConn, CommandType.Text, strQuery);
                        strQuery = "INSERT INTO TRN_MPSTENDER(IMPSTENDERID,VTOKEN,DTRANDATE,DTRANTIME,VCMDSTATUS,VTEXTRESPONSE,VPROCESSDATA,VAUTHCODE,NPURCHASEAMOUNT,NAUTHAMOUNT,VBATCHNO,NBATCHAMOUNT,VCARDTYPE,VCARDHOLDERNAME,VRETURNCODE,VSIGNATUREDATA,VTENDERTYPE,VVOUCHERNO,VMEMO,VTRANCODE,VCARDUSAGE,VAVSRESULT,VCAPTURESTATUS,VACQREFDATA,VOPERATORID,VPREPAIDACCOUNT,VUSERID,VINVNUMBER,VREFNUMBER,XMLRESPONSE,ITRANID,vtransfer) " +
                                "VALUES ('" + dr["ITRANID"] + "', '" + dr["VTOKEN"] + "'," + retunNull(dr["DTRANDATE"].ToString()) + ", '" + dr["DTRANTIME"] + "','" + dr["VCMDSTATUS"].ToString().Replace("'", "") + "', '" + dr["VTEXTRESPONSE"] + "','" + dr["VPROCESSDATA"] + "',  '" + dr["VAUTHCODE"].ToString().Replace("'", "") + "','" + dr["NPURCHASEAMOUNT"] + "'," +
                                "'" + dr["NAUTHAMOUNT"].ToString() + "','" + dr["VBATCHNO"].ToString() + "','" + dr["NBATCHAMOUNT"] + "','" + dr["VCARDTYPE"] + "','" + dr["VCARDHOLDERNAME"] + "','" + dr["VRETURNCODE"] + "'," + retunCharNull(dr["VSIGNATUREDATA"].ToString()) + ",'" + dr["VTENDERTYPE"] + "','" + dr["VVOUCHERNO"] + "'" +
                                ",'" + dr["VMEMO"] + "','" + dr["VTRANCODE"] + "','" + dr["VCARDUSAGE"] + "','" + dr["VAVSRESULT"].ToString().Replace("'", "") + "','" + dr["VCAPTURESTATUS"] + "','" + dr["VACQREFDATA"] + "'" +
                                ",'" + dr["VOPERATORID"].ToString().Replace("'", "") + "','" + dr["VPREPAIDACCOUNT"] + "','" + dr["VUSERID"] + "','" + dr["VINVNUMBER"].ToString().Replace("'", "") + "','" + dr["VREFNUMBER"] + "','" + dr["XMLRESPONSE"].ToString() + "','" + dr["ITRANID"] + "'" +
                                " ,'Yes')";

                       
                        //Execute Query
                        SqlHelper.ExecuteNonQuery(strSerConn, CommandType.Text, strQuery);

                        //Get the sales id which is already updated so at the edit we can update all record at once
                        strCreditID += dr["ITRANID"].ToString() + ",";
                    }
                }




                strCreditID = strCreditID.TrimEnd(',');
                if (strCreditID.Length > 0)
                {
                    strQuery = "Update TRN_MPSTENDER set vTransfer='Yes' where ITRANID in (" + strCreditID + ")";
                    SqlHelper.ExecuteNonQuery(strLocConn, CommandType.Text, strQuery);
                   
                }


            }
            catch (Exception e)
            {
               

                //If any transaction inserted online and if we get error then we are deleting all inserted transaction for this batch
                strCreditID = strCreditID.TrimEnd(',');
                if (strCreditID.Length > 0)
                {
                    //Delete header records
                    strQuery = "DELETE FROM TRN_MPSTENDER where  ITRANID in (" + strCreditID + ")";
                    SqlHelper.ExecuteNonQuery(strSerConn, CommandType.Text, strQuery);
                    lblStatus.Text = e.Message.ToString();
                    lblStatus.Refresh();

                }

               
                // SendEmail(adminEmail, "   SalesTrn Error:\n" + e.Message + "\n" + strQuery);
            }
        }
        private string retunCharNull(string str)
        {
            string str1 = string.Empty;
            str1 = str;
            if (str.Length == 0)
                str1 = "NULL";
            else
                str1 = "'" + str + "'";

            return str1;
        }
        private string retunNull(string str)
        {
            string str1 = string.Empty;
            str1 = str;
            if (str.Length == 0)
                str1 = "NULL";
            else
            {
             //   str1 = "'" + Convert.ToDateTime(str).ToString("MM-dd-yyyy") + "'";
                str1 = "'" + Convert.ToDateTime(str).ToString("yyyy-MM-dd") + "'";
            }
            return str1;
        }
        private string retunDateTimeNull(string str)
        {
            string str1 = string.Empty;
            str1 = str;
            if (str.Length == 0)
                str1 = "NULL";
            else
            {
               // str1 = "'" + Convert.ToDateTime(str).ToString("MM-dd-yyyy HH:mm:ss") + "'";
                str1 = "'" + Convert.ToDateTime(str).ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }

            return str1;
        }
        private void btnNewExtractData_Click(object sender, EventArgs e)
        {
            try
            {
                /*string strTableName = "";
                string strFullQuery = "";
                if (txtStrQuery.Text.ToLower().StartsWith("select"))
                {
                    DataTable dt = new DataTable();
                    strServerConn = "User=SYSDBA;Password=masterkey;Database=" + txtFileName.Text + ";DataSource=" + txtServerName.Text + ";Pooling=false";
                    dt = dlib.ExecuteDatTable(strServerConn, txtStrQuery.Text.Trim().ToString());
                    if (dt.Rows.Count > 0)
                    {
                        //Get TableName
                        dataGridView1.DataSource = dt.DefaultView;
                        string strQ = "";
                        strQ = txtStrQuery.Text.ToString();
                        strTableName = (strQ.Substring(strQ.IndexOf("from") + 4)).Trim(' ');
                        if (strTableName.IndexOf(' ') > 0)
                            strTableName = strTableName.Substring(0, strTableName.IndexOf(' '));
                        string strFileName = Application.StartupPath;
                        string strLine = "";
                        strFileName = strFileName + "\\" + strTableName + ".sql";
                        
                        string strColumnData = "";

                        if (File.Exists(strFileName))
                            File.Delete(strFileName);
                        
                        int col = 0;
                        char[] specialcahe = { '\0', '\r', '\n' };
                        try
                        {
                            foreach (DataRow dr in dt.Rows)
                            {


                                for (col = 0; col < dt.Columns.Count; col++)
                                {

                                    strColumnData = "'" +dr[col].ToString().TrimEnd(specialcahe)+"',";

                                }
                                strColumnData = Convert.ToString(strColumnData);

                                strLine = "insert into " + strTableName + "(XMLDTA ) " + "Values (" + strColumnData.TrimEnd(',') + ");";
                                //strColumnData="<?xml version=1.0 standalone=yes?><PLResponse><Result>APPROVED</Result><CardNumber>9222</CardNumber><CardType>Debit</CardType><Authorization>585807</Authorization><Amount>11.95</Amount><AuthAmt>11.95</AuthAmt><RecNum>14000</RecNum><RefData>000001</RefData><CustomerName>COOK/SCOTT A</CustomerName><Command>DCSALE</Command><ResultText>Transaction Approved </ResultText><Id>206302015054140</Id><ErrorCode>0000</ErrorCode><TransactionDate>063015</TransactionDate><TransactionTime>0541</TransactionTime></PLResponse>";

                                strFullQuery += strColumnData;
                               // strLine = strColumnData;
                                //dlib.UpdateXMLDATA(strServerConn, dr);
                                TextWriter txtWrite = new StreamWriter(strFileName, true);
                                txtWrite.WriteLine(strLine);
                                txtWrite.Close();
                            }
                        }
                        catch (Exception inn)
                        {
                            MessageBox.Show(inn.Message);
                            lblStatus.Text = inn.Message.ToString();
                            lblStatus.Refresh();
                        }
                        lblStatus.Text = "Completed";
                        lblStatus.Refresh();

                        txtStrQuery.Text = strFullQuery;
                    }

                }*/
                CreditCardTransaction();
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.Message.ToString();
                lblStatus.Refresh();
            }
        }
        
        private string retunTimeNull(string str)
        {
            string str1 = string.Empty;
            str1 = str;
            if (str.Length == 0)
                str1 = "NULL";
            else
                str1 = "'" + Convert.ToDateTime(str).ToString("HH:mm:ss") + "'";
            return str1;
        }

        private string retunByteNull(string str)
        {
            string str1 = string.Empty;
            str1 = str;
            if (str.Length == 0)
                str1 = "NULL";
            else
                str1 = "'" + Convert.ToString(str)+ "'";
            return str1;
        }

        private string retunZero(string str)
        {
            string str1 = string.Empty;
            str1 = str;
            if (str.Length == 0)
                str1 = "0";
            else
                str1 =  str ;
            return str1;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtStrQuery.Text = "";
            dataGridView1.DataSource = null;
            txtStrQuery.Focus();
        }

        private void btntran_Click(object sender, EventArgs e)
        {
            try
            {
                string vcatecode ="";
                string vdepcode = "";
                string vcatename = "";
                string vdepname = "";
                string strFileName = Application.StartupPath;
                strFileName = strFileName+"\\LogDetail.txt";

                string strLine = "";
                if (CheckValue())
                {
                    strServerConn = "User=SYSDBA;Password=masterkey;Database=" + txtFileName.Text + ";DataSource=" + txtServerName.Text + ";Pooling=false";
                    string strQuery = "select  vitemname,vitemcode,idettrnid from trn_salesdetail where (vdepcode is null or vdepcode='')";
                    if (txtStrQuery.Text.ToString().Length > 0)
                        strQuery = txtStrQuery.Text.ToString();

                    DataTable dtREC = dlib.ExecuteDatTable(strServerConn, strQuery);
                    foreach (DataRow dr in dtREC.Rows)
                    {
                        strQuery = "select vcategorycode,vdepcode from mst_item where vitemname='" + dr["vitemname"].ToString() + "' and vitemcode='"+dr["vitemcode"].ToString()+"'";
                        //strQuery = "select vcategorycode,vdepcode from mst_item where  vitemcode='" + dr["vitemcode"].ToString() + "'";
                        DataTable dtItem = dlib.ExecuteDatTable(strServerConn, strQuery);
                        if (dtItem.Rows.Count > 0)
                        {
                            foreach (DataRow drItem in dtItem.Rows)
                            {
                                vcatecode = "";
                                vdepcode = "";
                                vcatename = "";
                                vdepname = "";
                                vcatecode = drItem["vcategorycode"].ToString();
                                vdepcode = drItem["vdepcode"].ToString();
                                DataTable dtCat = dlib.ExecuteDatTable(strServerConn, "select vcategorycode,vcategoryname from mst_Category where vcategorycode='" + vcatecode + "'");
                                DataTable dtDep = dlib.ExecuteDatTable(strServerConn, "select vdepcode,vdepartmentname from mst_Department  where vdepcode='" + vdepcode + "'");
                                if (dtCat.Rows.Count > 0)
                                    vcatename = dtCat.Rows[0]["vcategoryname"].ToString();
                                if (dtDep.Rows.Count > 0)
                                    vdepname = dtDep.Rows[0]["vdepartmentname"].ToString();

                                strQuery = "update trn_salesdetail set vdepcode='" + vdepcode + "',vcatcode='" + vcatecode + "',vcatname='" + vcatename + "',vdepname='" + vdepname + "' where idettrnid='" + dr["idettrnid"].ToString() + "' ";
                                dlib.ExecuteNonQuery(strServerConn, strQuery);

                                lblStatus.Text = "Update " + dr["vitemname"].ToString();
                                lblStatus.Refresh();
                            }
                        }
                        else
                        {
                            TextWriter txtWrite = new StreamWriter(strFileName, true);
                            strLine = dr["vitemname"].ToString() + "  " + dr["vitemcode"].ToString();
                            txtWrite.WriteLine(strLine);
                            txtWrite.Close();
                        }
                    }
                }

                lblStatus.Text = "Finish";
                lblStatus.Refresh();
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.Message.ToString();
                lblStatus.Refresh();
            }
            
        }

        private void btncsvExport_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            lblStatus.Text = "";
            lblStatus.Refresh();
            if (txtStrQuery.Text.ToLower().StartsWith("select"))
                {
                   
                    strServerConn = "User=SYSDBA;Password=masterkey;Database=" + txtFileName.Text + ";DataSource=" + txtServerName.Text + ";Pooling=false";
                    string strQ = "";
                    strQ = txtStrQuery.Text.ToString();
                    dt = dlib.ExecuteDatTable(strServerConn, strQ.Trim().ToString());
                    if (dt.Rows.Count > 0)
                    {
                        string strTableName = (strQ.Substring(strQ.ToLower().IndexOf("from") + 4)).Trim(' ');
                        if (strTableName.IndexOf(' ') > 0)
                            strTableName = strTableName.Substring(0, strTableName.IndexOf(' '));
                        //strTableName = "DataCSV";
                        StreamWriter wr = new StreamWriter(Application.StartupPath + "\\"+strTableName+".xls");

                        try
                        {
                            lblStatus.Text = "Exporting Data";
                            lblStatus.Refresh();
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                wr.Write(dt.Columns[i].ToString().ToUpper() + "\t");
                            }

                            wr.WriteLine();

                            //write rows to excel file
                            for (int i = 0; i < (dt.Rows.Count); i++)
                            {
                                for (int j = 0; j < dt.Columns.Count; j++)
                                {
                                    if (dt.Rows[i][j] != null)
                                    {
                                        wr.Write(Convert.ToString(dt.Rows[i][j]) + "\t");
                                    }
                                    else
                                    {
                                        wr.Write("\t");
                                    }
                                }
                                //go to next line
                                wr.WriteLine();
                            }
                            //close file
                            wr.Close();
                            lblStatus.Text = "Finish Exporting Data";
                            lblStatus.Refresh();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
            }
        }

        private void btnBackupFolder_Click(object sender, EventArgs e)
        {
            /*FolderBrowserDialog fld = new FolderBrowserDialog();
           
            if (fld.ShowDialog() == DialogResult.OK)
            {
                txtBackupFolder.Text = fld.SelectedPath;
            }*/
           
            if (chkGateway.Checked)
            {
                POSPayment.PosPayment posPaY = new POSPayment.PosPayment();
                posPaY.InitSetting(Convert.ToInt32(txtBackupFolder.Text.ToString()));
                lblStatus.Text = "Closing Batch with Gatway:" + Convert.ToInt32(cmbBatch.Text.ToString());
                lblStatus.Refresh();
                string textResponse = posPaY.batchClose("ALL", "BATCHCLOSE", Convert.ToInt32(cmbBatch.Text.ToString()), Convert.ToInt32(txtBackupFolder.Text.ToString()));
                if (textResponse == "ERROR")
                {
                    lblStatus.Text = "Error with Gatway:" + Convert.ToInt32(cmbBatch.Text.ToString());
                    lblStatus.Refresh();
                    return;
                }
            }
            
                lblStatus.Text = "Closing Batch :" + Convert.ToInt32(cmbBatch.Text.ToString());
                lblStatus.Refresh();
                CloseBatch(cmbBatch.Text.ToString().Trim());
                lblStatus.Text = "Close Batch :" + Convert.ToInt32(cmbBatch.Text.ToString());
                lblStatus.Refresh();
            
            LoadBatch();
        }
        private void LoadBatch()
        {
          //DataTable dt = dlib.ExecuteDatTable(strServerConn, "select ibatchid from trn_batch where eStatus='Open'");
            //DataTable dt = dlib.ExecuteDatTable(strServerConn, "select ibatchid from trn_batch");
            DataTable dt1 = dlib.ExecuteDatTable(strServerConn, "select ibatchid from trn_batch where eStatus='Open'");
            if (dt1.Rows.Count == 0)
            {
              dt1= dlib.ExecuteDatTable(strServerConn, txtStrQuery.Text.ToString());
              txtStrQuery.Text = "";
            }
            if (dt1.Rows.Count > 0)
            {
                cmbBatch.DataSource = dt1;
                cmbBatch.DisplayMember = "ibatchid";
                cmbBatch.ValueMember = "ibatchid";
            }       
        }
        public void writeLogMessage(String logMessage)
        {
            string path ="C:\\POS2020Log" + @"\PurgeLogFile.txt";
            logMessage = "[ "+DateTime.Now.ToString()+" ] " +logMessage  ;
            if (!File.Exists(path))
            {
                using (StreamWriter tw = File.CreateText(path))
                {
                    tw.WriteLine(logMessage);
                    tw.Close();
                }
            }
            else
            {
                TextWriter tr = new StreamWriter(path,true);
                tr.WriteLine(logMessage);
                tr.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
            

       
        
    }
}
