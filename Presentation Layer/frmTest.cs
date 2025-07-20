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
            clsTables Tables = clsTables.GetObject("Server =.;Database = Inventory;User Id = sa ; Password = Eslamyasse1");
            clsTable Table = Tables.GetTable("Categories");
            IDataAccessLayerCodeGenerator[] DataAccessCodeGenerators = {new clsDataAccessLayerInsertCodeGenerator() , new clsDataAccessLayerUpdateCodeGenerator() ,
            new clsDataAccessLayerFindCodeGenerator() , new clsDataAccessLayerListCodeGenerator() , new clsDataAccessLayerDeleteCodeGenerator()
            ,new clsDataAccessLayerIsExistsCodeGenerator()};
            IBusinessLayerCodeGenerator[] BusinessCodeGenerators = { new clsBusinessLayerFindCodeGenerator() , new clsBusinessLayerDeleteCodeGenerator()
            ,new clsBusinessLayerListCodeGenerator() , new clsBusinessLayerUpdateCodeGenerator() , new clsBusinessLayerInsertCodeGenerator()};
            ICodeGenerator[] CodeGenerators = { new clsDataAccessCodeGenerator(DataAccessCodeGenerators) , new clsBusinessLayerCodeGenerator(BusinessCodeGenerators)  };
            Table.GenerateCode( CodeGenerators , "Category");
            
        }
    }
}
