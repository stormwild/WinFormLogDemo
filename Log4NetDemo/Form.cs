using Log4NetDemo.BusinessLayer;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Log4NetDemo
{
    public partial class Form : System.Windows.Forms.Form
    {
        private readonly ILogger<Form> _logger;
        private readonly IMainService _service;

        public Form(ILogger<Form> logger, IMainService service)
        {
            _logger = logger;
            _service = service;

            InitializeComponent();
        }

        private void ShowMessageButton_Click(object sender, EventArgs e)
        {
            try
            {
                _logger.LogInformation("Button clicked");

                _service.DoTask();

                MessageBox.Show("Button Clicked");

                _logger.LogInformation("DoTask done");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
