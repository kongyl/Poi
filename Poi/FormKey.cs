using Poi.Dao;
using Poi.Model;
using System;
using System.Windows.Forms;

namespace Poi
{
    public partial class FormKey : Form
    {
        public FormKey()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            string ak = textBoxAk.Text;
            string sk = textBoxSk.Text;

            if (string.IsNullOrEmpty(ak))
            {
                MessageBox.Show("AK 不能为空");
                return;
            }

            if (string.IsNullOrEmpty(sk))
            {
                MessageBox.Show("SK 不能为空");
                return;
            }

            Dictionary dicAk = new Dictionary();
            dicAk.Key = "ak";
            dicAk.Value = ak;
            DictionaryDao.UpdateByKey(dicAk);
            Dictionary dicSk = new Dictionary();
            dicSk.Key = "sk";
            dicSk.Value = sk;
            DictionaryDao.UpdateByKey(dicSk);

            MessageBox.Show("配置完成");
            DialogResult = DialogResult.OK;
        }
    }
}
