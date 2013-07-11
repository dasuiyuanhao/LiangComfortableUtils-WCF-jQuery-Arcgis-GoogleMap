using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LiangComUtils.Web.Controls.AllPage
{
    public partial class Header : System.Web.UI.UserControl
    {
        #region Properties

        /// <summary>
        /// 主菜单目录导航-当前的索引
        /// </summary>
        public string MainNavigationIndex
        {
            get
            {
                return HiddenFieldMainNavigationIndex.Value;
            }
            set
            {
                try
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        HiddenFieldMainNavigationIndex.Value = value;
                    }
                }
                catch { }
            }
        }

        #endregion Properties

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion Event Handlers

        #region Methods


        #endregion Methods
    }
}