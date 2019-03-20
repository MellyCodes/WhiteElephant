/**
* @project White Elephant E-Commerce Website
* @authors Courtney Diotte
* @authors Melanie Roy-Plommer
* @version 1.0
*
* @section DESCRIPTION
* <  >
*
* @section LICENSE
* Copyright 2018 - 2019
* Permission to use, copy, modify, and/or distribute this software for
* any purpose with or without fee is hereby granted, provided that the
* above copyright notice and this permission notice appear in all copies.
*
* THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
* WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
* MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
* ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
* WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
* ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
* OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
*
* @section Academic Integrity
* I certify that this work is solely my own and complies with
* NBCC Academic Integrity Policy (policy 1111)
*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WhiteElephantWebsite
{
    public static class DBHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string ConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;
        }

        /// <summary>
        /// Execute scalar query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStatement"></param>
        /// <param name="prms"></param>
        /// <param name="cmdType"></param>
        /// <returns></returns>
        public static T GetScalarValue<T>(string sqlStatement, SqlParameter[] prms
            , CommandType cmdType = CommandType.StoredProcedure)
        {
            object returnValue = null;
            T value;

            using (SqlConnection conn = new SqlConnection(ConnectionString()))
            {
                //Call the getArtists SProc from the artist
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sqlStatement;
                    cmd.CommandType = cmdType;

                    if (prms != null)
                        cmd.Parameters.AddRange(prms);

                    conn.Open();

                    returnValue = cmd.ExecuteScalar();

                    if (returnValue == DBNull.Value)
                        returnValue = null;

                    conn.Close();//Not necessary as connection is within using
                }
            }

            if (returnValue != null)
                value = (T)returnValue;
            else
                value = default(T);

            return value;
        }

        /// <summary>
        /// Get a Single value from a query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStatement"></param>
        /// <param name="ColumnName"></param>
        /// <param name="prms"></param>
        /// <param name="cmdType"></param>
        /// <returns></returns>
        public static T GetQueryValue<T>(string sqlStatement, string ColumnName
        , SqlParameter[] prms, CommandType cmdType = CommandType.StoredProcedure)
        {
            object returnValue = null;
            T value;

            using (SqlConnection conn = new SqlConnection(ConnectionString()))
            {
                //Call the getArtists SProc from the artist
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sqlStatement;
                    cmd.CommandType = cmdType;

                    if (prms != null)
                        cmd.Parameters.AddRange(prms);

                    conn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                if (dr[ColumnName] != DBNull.Value)
                                    returnValue = dr[ColumnName];
                            }
                        }
                    }

                    conn.Close();//Not necessary as connection is within using
                }
            }

            if (returnValue != null)
                value = (T)returnValue;
            else
                value = default(T);

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlStatement"></param>
        /// <param name="prms"></param>
        /// <param name="cmdType"></param>
        /// <returns></returns>
        public static DataTable GetQuery(string sqlStatement, SqlParameter[] prms, CommandType cmdType = CommandType.StoredProcedure)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionString()))
            {
                //Call the getArtists SProc from the artist
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sqlStatement;
                    cmd.CommandType = cmdType;

                    if (prms != null)
                        cmd.Parameters.AddRange(prms);

                    conn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dt.Load(dr);
                        }

                        conn.Close();//Not necessary as connection is within using
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// Run table scripts and sproc for Guid Table Galleries
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStatement"></param>
        /// <param name="OutputParam"></param>
        /// <param name="prms"></param>
        /// <param name="cmdType"></param>
        /// <returns></returns>
        public static T Insert<T>(string sqlStatement, string OutputParam
        , SqlParameter[] prms, CommandType cmdType = CommandType.StoredProcedure)
        {
            object returnId = null;
            T id;

            using (SqlConnection conn = new SqlConnection(ConnectionString()))
            {
                //Call the getArtists SProc from the artist
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sqlStatement;
                    cmd.CommandType = cmdType;

                    if (prms != null)
                        cmd.Parameters.AddRange(prms);

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    returnId = cmd.Parameters[OutputParam].Value;

                    conn.Close();//Not necessary as connection is within using
                }
            }

            if (returnId != null)
                id = (T)returnId;
            else
                id = default(T);

            return id;
        }

        /// <summary>
        /// Insert with no return needed
        /// </summary>
        /// <param name="sqlStatement"></param>
        /// <param name=""></param>
        /// <param name="prms"></param>
        /// <param name="cmdType"></param>
        public static void Insert(string sqlStatement
        , SqlParameter[] prms, CommandType cmdType = CommandType.StoredProcedure)
        {


            using (SqlConnection conn = new SqlConnection(ConnectionString()))
            {
                //Call the getArtists SProc from the artist
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sqlStatement;
                    cmd.CommandType = cmdType;

                    if (prms != null)
                        cmd.Parameters.AddRange(prms);

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    conn.Close();//Not necessary as connection is within using
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlStatement"></param>
        /// <param name="prms"></param>
        /// <param name="cmdType"></param>
        /// <returns></returns>
        public static List<SqlParameter> NonQuery(string sqlStatement
        , SqlParameter[] prms, CommandType cmdType = CommandType.StoredProcedure)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString()))
            {
                //Call the getArtists SProc from the artist
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sqlStatement;
                    cmd.CommandType = cmdType;

                    if (prms != null)
                        cmd.Parameters.AddRange(prms);

                    conn.Open();

                    cmd.ExecuteNonQuery();
                    conn.Close();//Not necessary as connection is within using
                }
            }

            //Return the output params
            return prms.Where(p => p.Direction == ParameterDirection.Output).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindableControl"></param>
        /// <param name="sqlStatement"></param>
        /// <param name="cmdType"></param>
        /// <param name="prms"></param>
        public static void DataBinding(object bindableControl, string sqlStatement
            , SqlParameter[] prms = null
            , CommandType cmdType = CommandType.StoredProcedure)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString()))
            {
                //Call the getArtists SProc from the artist
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sqlStatement;
                    cmd.CommandType = cmdType;

                    if (prms != null)
                        cmd.Parameters.AddRange(prms);

                    conn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            BindControl(bindableControl, dr);
                        }
                    }

                    conn.Close();//Not necessary as connection is within using
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindableControl"></param>
        /// <param name="sqlStatement"></param>
        /// <param name="prms"></param>
        /// <param name="cmdType"></param>
        public static void DataBindingWithPaging(object bindableControl, string sqlStatement
            , SqlParameter[] prms = null
            , CommandType cmdType = CommandType.StoredProcedure)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString()))
            {
                //Call the getArtists SProc from the artist
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sqlStatement;
                    cmd.CommandType = cmdType;

                    if (prms != null)
                        cmd.Parameters.AddRange(prms);

                    conn.Open();

                    var da = new SqlDataAdapter(cmd);
                    var ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables.Count > 0)
                    {
                        BindControl(bindableControl, ds.Tables[0]);
                    }

                    conn.Close();//Not necessary as connection is within using
                }
            }
        }

        /// <summary>
        /// BaseDataBoundControl
        /// GridView
        /// DropDownList 
        /// 
        /// BaseDataList
        /// DetailView
        /// </summary>
        /// <param name="bindableControl"></param>
        /// <param name="dr"></param>
        private static void BindControl(object bindableControl, SqlDataReader dr)
        {
            if (bindableControl is BaseDataBoundControl)
            {
                ((BaseDataBoundControl)bindableControl).DataSource = dr;
                ((BaseDataBoundControl)bindableControl).DataBind();
            }

            if (bindableControl is BaseDataList)
            {
                ((BaseDataList)bindableControl).DataSource = dr;
                ((BaseDataList)bindableControl).DataBind();
            }

            if (bindableControl is Repeater)
            {
                ((Repeater)bindableControl).DataSource = dr;
                ((Repeater)bindableControl).DataBind();
            }
        }

        /// <summary>
        /// BaseDataBoundControl
        /// GridView
        /// DropDownList 
        /// 
        /// BaseDataList
        /// DetailView
        /// 
        /// Using datatable to allow paging
        /// </summary>
        /// <param name="bindableControl"></param>
        /// <param name="dt">DataTable of data to bind</param>
        private static void BindControl(object bindableControl, DataTable dt)
        {
            if (bindableControl is BaseDataBoundControl)
            {
                ((BaseDataBoundControl)bindableControl).DataSource = dt;
                ((BaseDataBoundControl)bindableControl).DataBind();
            }

            if (bindableControl is BaseDataList)
            {
                ((BaseDataList)bindableControl).DataSource = dt;
                ((BaseDataList)bindableControl).DataBind();
            }

            if (bindableControl is Repeater)
            {
                ((Repeater)bindableControl).DataSource = dt;
                ((Repeater)bindableControl).DataBind();
            }

        }

        #region [Common Category SqlParams]


        public static SqlParameter SetCategoryNameParam(string value)
        {
            return new SqlParameter()
            {
                ParameterName = "@CategoryName",
                SqlDbType = SqlDbType.NVarChar,
                Size = 50,
                Value = value
            };
        }

        public static SqlParameter SetCategoryDescParam(string value)
        {
            return new SqlParameter()
            {
                ParameterName = "@Description",
                SqlDbType = SqlDbType.NVarChar,
                Value = value
            };
        }


        #endregion

        #region [Common Product SqlParams]

        public static SqlParameter SetProductIdParam(int value)
        {
            return new SqlParameter()
            {
                ParameterName = "@ProductId",
                SqlDbType = SqlDbType.Int,
                Value = value
            };
        }

        public static SqlParameter SetProductNameParam(string value)
        {
            return new SqlParameter()
            {
                ParameterName = "@ProductName",
                SqlDbType = SqlDbType.NVarChar,
                Size = 50,
                Value = value
            };
        }

        public static SqlParameter SetProductBriefDescParam(string value)
        {
            return new SqlParameter()
            {
                ParameterName = "@ProductBriefDesc",
                SqlDbType = SqlDbType.NVarChar,
                Value = value
            };
        }

        public static SqlParameter SetProductFullDescParam(string value)
        {
            return new SqlParameter()
            {
                ParameterName = "@ProductFullDesc",
                SqlDbType = SqlDbType.NVarChar,
                Value = value
            };
        }

        public static SqlParameter SetProductStatusCodeParam(bool value)
        {
            return new SqlParameter()
            {
                ParameterName = "@StatusCode",
                SqlDbType = SqlDbType.Bit,
                Value = value
            };
        }

        public static SqlParameter SetProductPriceParam(double value)
        {
            return new SqlParameter()
            {
                ParameterName = "@ProductPrice",
                SqlDbType = SqlDbType.Money,
                Value = value
            };
        }

        public static SqlParameter SetProductFeaturedParam(bool value)
        {
            return new SqlParameter()
            {
                ParameterName = "@Featured",
                SqlDbType = SqlDbType.Bit,
                Value = value
            };
        }

        public static SqlParameter SetCategoryIdParam(int value)
        {
            return new SqlParameter()
            {
                ParameterName = "@CategoryId",
                SqlDbType = SqlDbType.Int,
                Value = value
            };
        }

        public static SqlParameter SetProductImageName(string value)
        {
            return new SqlParameter()
            {
                ParameterName = "@ImageName",
                SqlDbType = SqlDbType.NVarChar,
                Size = 50,
                Value = value
            };
        }

        public static SqlParameter SetProductImageDate(DateTime value)
        {
            return new SqlParameter()
            {
                ParameterName = "@ImageUploadDate",
                SqlDbType = SqlDbType.DateTime,
                Value = value
            };
        }

        public static SqlParameter SetProductImageAlt(string value)
        {
            return new SqlParameter()
            {
                ParameterName = "@AltText",
                SqlDbType = SqlDbType.NVarChar,
                Size = 50,
                Value = value
            };
        }

        #endregion
    }
}