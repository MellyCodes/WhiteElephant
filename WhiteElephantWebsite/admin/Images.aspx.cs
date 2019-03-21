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
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ImageD = System.Drawing.Image;

namespace WhiteElephantWebsite.admin
{
    public partial class ImageApproval : AdminPage
    {
        List<ListItem> files = new List<ListItem>();
        private string sourcePath;
        private string destinationPath;

        protected void Page_Load(object sender, EventArgs e)
        {

            base.Page_Load(sender, e);

            if (!IsPostBack)
            {
                LoadFilesFromTempDir();
            }
        }

        private void LoadFilesFromTempDir() { 

            string[] filePaths = Directory.GetFiles(Server.MapPath("~/tempimages"));

            foreach (string filePath in filePaths)
            {
                string fileName = Path.GetFileName(filePath);
                files.Add(new ListItem(fileName, "~/tempimages" + fileName));
            }

            ddlImages.Items.Clear();

            //ddlImages.Items.Add(new ListItem("-- Select Image --", "-1"));
            ddlImages.Items.Insert(0, new ListItem("-- Select Image --", "-1"));

            ddlImages.DataSource = files;
            ddlImages.DataBind();



        }



        protected void btnMove_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblError.Text = "";
            this.sourcePath = $@"{Server.MapPath("~/tempimages")}/{this.ddlImages.SelectedValue.ToString()}";
            this.destinationPath = $@"{Server.MapPath("~/images")}/{this.ddlImages.SelectedValue.ToString()}";


            File.Move(sourcePath, destinationPath);
            lblMessage.Text = "Image Approved";

            imgImageUpload.ImageUrl = "";

            LoadFilesFromTempDir();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

            lblMessage.Text = "";
            lblError.Text = "";
            try
            {
                string targetPath = $@"{Server.MapPath("~/tempimages")}";

                if (Directory.Exists(targetPath))
                {
                    if (uplImage.HasFile)
                    {
                        int fileLength = uplImage.PostedFile.ContentLength;
                        ImageD img = ImageD.FromStream(uplImage.PostedFile.InputStream);

                        string imageType = "";

                        if (ImageFormat.Jpeg.Equals(img.RawFormat))
                            imageType = "JPEG";

                        if (ImageFormat.Png.Equals(img.RawFormat))
                            imageType = "PNG";

                        if (ImageFormat.Gif.Equals(img.RawFormat))
                            imageType = "GIF";

                        if (File.Exists($@"{targetPath}\{uplImage.FileName}"))
                        {
                            this.lblMessage.Text = "File exists and will be overwritten. ";
                        }

                        uplImage.SaveAs($@"{targetPath}\{uplImage.FileName}");

                        this.lblMessage.Text += $"{uplImage.FileName}" +
                                                $" has been uploaded to the physical directory " +
                                                $"{targetPath}. " +
                                                $"The size is " +
                                                $"{fileLength.ToString()} bytes " +
                                                $"and the filetype is {imageType}";
                    }
                }
                else
                {
                    this.lblError.Text = "No image selected";
                }

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected void ddlImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.sourcePath = $"~/tempimages/{this.ddlImages.SelectedValue.ToString()}";
            imgImageUpload.ImageUrl = sourcePath;
        }

        protected void btnDeleteImage_Click(object sender, EventArgs e)
        {
            string file_name = ddlImages.SelectedItem.Text;
            string path = $@"{Server.MapPath("~/tempimages")}/{file_name}";
            FileInfo file = new FileInfo(path);
            if (file.Exists)//check if file exists
            {
                this.imgImageUpload.ImageUrl = "";
                file.Delete();
                LoadFilesFromTempDir();
                this.lblMessage.Text = file_name + " file deleted successfully";
            }
            else
            {
                this.lblMessage.Text = file_name + " This file does not exists ";
            }
        }
    }
}