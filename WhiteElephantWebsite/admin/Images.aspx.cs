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
    }
}