using Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation_Layer
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
            clsTables Tables = clsTables.GetObject("Server =.;Database = HR;User Id = sa ; Password = Eslamyasse1");
            clsTable Table = Tables.GetTable("Employees");
            IDataAccessLayerCodeGenerator[] DataAccessCodeGenerators = {new clsDataAccessLayerInsertCodeGenerator()  ,
            new clsDataAccessLayerFindCodeGenerator() , new clsDataAccessLayerListCodeGenerator() , new clsDataAccessLayerDeleteCodeGenerator()
            ,new clsDataAccessLayerIsExistsCodeGenerator() , new clsDataAccessLayerUpdateCodeGenerator()};
            IBusinessLayerCodeGenerator[] BusinessCodeGenerators = { new clsBusinessLayerFindCodeGenerator() , new clsBusinessLayerDeleteCodeGenerator()
            ,new clsBusinessLayerListCodeGenerator() , new clsBusinessLayerInsertCodeGenerator()
            ,new clsBusinessLayerIsExistsCodeGenerator() , new clsBusinessLayerUpdateCodeGenerator()};
            ICodeGenerator[] CodeGenerators = { new clsDataAccessCodeGenerator(DataAccessCodeGenerators) , new clsBusinessLayerCodeGenerator(BusinessCodeGenerators)  };
            Table.GenerateCode( CodeGenerators , "Employee");
            
        }
    }
}
