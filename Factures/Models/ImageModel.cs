using Factures.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factures.Models
{
    public class ImageModel : Model
    {
        private int _id;
        private string _name;
        private string _type;
        private string _data;

        public ImageModel()
        {
            this.Table = "images";
            this.Fillable = new string[3];
            this.Fillable[0] = "name";
            this.Fillable[1] = "type";
            this.Fillable[2] = "data";
        }

        #region
        public string Image
        {
            get { return _data; }
            set { _data = value; }
        }


        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }


        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        #endregion

        #region
        public void FillMeById()
        {
            DataTable me = this.Find(Id);
            Name = me.Rows[0][1].ToString();
            Type = me.Rows[0][2].ToString();
            Image = me.Rows[0][3].ToString();
        }

        private List<KeyValuePair<string, string[]>> FillMe()
        {
            List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>();
            // Important
            if (Name == null || Type == null || Image == null)
                return null;
            string[] value = new string[2] { "", Name };
            Data.Add(new KeyValuePair<string, string[]>("name", value));
            value = new string[2] { "", Type };
            Data.Add(new KeyValuePair<string, string[]>("type", value));
            value = new string[2] { "", Image };
            Data.Add(new KeyValuePair<string, string[]>("data" +
                "", value));
            return Data;
        }

        private List<KeyValuePair<string, string[]>> Primaries()
        {
            string[] value = new string[2];
            value[0] = "number";
            value[1] = Id.ToString();
            List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>
            {
                new KeyValuePair<string, string[]>("id", value)
            };
            return Data;
        }
        #endregion

        #region
        public string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        public byte[] Base64ToByteArr(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = System.Drawing.Image.FromStream(ms, true);
            return this.ImageToByteArray(image);
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
        #endregion

        #region
        public ImageModel SaveThis()
        {
            if (Name == null || Name == string.Empty || Type == null || Image == null)
                return null;
            DataTable dt = this.Save(this.FillMe());
            this.Id = Convert.ToInt32(dt.Rows[0][0].ToString());
            return this;
        }

        public ImageModel UpdateThis()
        {
            this.Update(this.FillMe(), this.Primaries());
            return this;
        }

        public bool Delete()
        {
            return this.DeleteThis(this.Primaries());
        }
        #endregion
    }
}
