using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using FireBird.SQLHelper;
using System.IO;
using Microsoft.Win32;
namespace FirebirdTool
{
    class POSDataLib
    {
        #region Set
        public DataSet ExecuteDataset(string Conn, string Squery)
        {
            DataSet ds = new DataSet();
            try
            {
               //for Datahelper
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.Text, Squery);
                return ds;
            }
            catch (Exception)
            {
                return ds;
            }
        }
        #endregion Set

        public DataTable ExecuteDatTable(string Conn, string Squery)
        {
            DataTable dt = new DataTable();
            try
            {//foe Dataset
                DataSet ds = new DataSet();

                ds = SqlHelper.ExecuteDataset(Conn, CommandType.Text, Squery);
                if (ds.Tables.Count > 0)
                    dt = ds.Tables[0];
                return dt;
            }
            catch (Exception)
            { 
                return dt;
                throw;
            }
            
               
            
        }
        public int ReturnID(string Conn, string Squery, string vfield)
        {
            int iID = 0;
            try
            {
             //for Retiom
                DataSet ds = new DataSet();
                FbParameter[] objParams = new FbParameter[1];
                objParams[0] = new FbParameter(vfield, SqlDbType.Int);
                objParams[0].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, Squery, objParams);
                iID = (int)(objParams[0].Value);
                return iID;
                
            }
            catch (Exception)
            {
                return iID;
            }
        }
        public void StoreProcedure(string Conn, string cmdStr, string sParammeter)
        {
            // gblFunction.gblServerisRunning = ChekRunningSrver();
            FbParameter[] objParams1 = new FbParameter[1];
            objParams1 = SqlHelperParameterCache.GetSpParameterSet(Conn, cmdStr, true);
            objParams1[0].Value = sParammeter;
            //int ret = 0;   
            //ret=SqlHelper.ExecuteNonQuery(serverconn,cmdStr,objParams1);
            DataSet obj6;
            obj6 = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure, cmdStr, objParams1);
        }
        public Boolean CheckRecordExist(string Conn, string Squery)
        {
            //  gblFunction.gblServerisRunning = ChekRunningSrver();
            DataTable custDS = new DataTable();
            try
            {
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(Conn, CommandType.Text, Squery);

                custDS = ds.Tables[0];
                if (custDS.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            finally
            {
                custDS.Dispose();
            }
        }

        public void ExecuteNonQuery(string Conn, string Squery)
        {
            try
            {
               
                SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, Squery);
            }
            catch (Exception)
            {
                throw ;
            }
        }
        public void UpdateImage(string Conn, DataRow drImage)
        {
            // MemoryStream msData = null;
            byte[] content = (byte[])drImage["ITEMIMAGE"];         

            FbParameter[] objParams1 = new FbParameter[2];
            objParams1 = SqlHelperParameterCache.GetSpParameterSet(Conn, "MST_UPDATEIMAGE", true);
            objParams1[0].Value = drImage["IITEMID"];
            objParams1[1].Value = content;
            //int ret = 0;   
            SqlHelper.ExecuteNonQuery(Conn, "MST_UPDATEIMAGE", objParams1);

        }

        public void UpdateXMLDATA(string Conn, DataRow drImage)
        {
            // MemoryStream msData = null;
            

            FbParameter[] objParams1 = new FbParameter[2];
            objParams1 = SqlHelperParameterCache.GetSpParameterSet(Conn, "USP_UPDATEXMLDATA", true);
             objParams1[0].Value = drImage["ITRANID"]; 
            //objParams1[0].Value = 1506302249;
            objParams1[1].Value = drImage["XMLRESPONSE"];
            //int ret = 0;   
            SqlHelper.ExecuteNonQuery(Conn, "USP_UPDATEXMLDATA", objParams1);

        }
      
    }
}
