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

        private string filePath;
        private string fileName;
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
            List<ListItem> files = new List<ListItem>();
            foreach (string filePath in filePaths)
            {
                string fileName = Path.GetFileName(filePath);
                files.Add(new ListItem(fileName, "~/tempimages" + fileName));
            }
            ddlImages.DataSource = files;
            ddlImages.DataBind();



        }



        protected void btnMove_Click(object sender, EventArgs e)
        {
            //string sourceFile = "~/tempimages/" ;
            //string destinationFile = @"C:\Users\Public\private\test.txt";

            // To move a file or folder to a new location:
            //System.IO.File.Move(sourceFile, destinationFile);

            File.Move(filePath, "~/images/" + fileName);

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
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
            //filePath = "~/tempimages/" + this.ddlImages.SelectedValue.ToString();
            filePath = $@"{Server.MapPath("~/tempimages")}/{this.ddlImages.SelectedValue.ToString()}";

            imgImageUpload.ImageUrl = filePath;
        }
    }
}