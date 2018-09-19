using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Developer : Yuan Chen (Emerald 27044044)
// This project is to build a Windows Form App to store,display,update,delete, add and search customers' info

namespace Customer_Form
{
    public partial class Form1 : Form
    {
        // List<T> of Custermer object called CustomerDB
        List<Customer> CustomerDB = new List<Customer>();

        //  LoadDB method
        public void LoadDB()
        {
            // CustomerDB.Add(new Customer("Full Name", "Phone No.", " "));
            CustomerDB.Add(new Customer("Jaarna", "Kereopa", "123-2514"));
            CustomerDB.Add(new Customer("Sue", "Stook", "123-1263"));
            CustomerDB.Add(new Customer("Jamie", "Allom", "123-3658"));
            CustomerDB.Add(new Customer("Brian", "Janes", "123-9898"));
        }
        // ClearBoxes method
        public void ClearBoxes()
        {
            txtFName.Clear();
            txtLName.Clear();
            txtPhone.Clear();
        }
        // ClearDisplay method
        public void ClearDisplay()
        {
            listBox.Items.Clear();
        }
        // DisplayCustomers method
        public void DisplayCustomers()
        {
            CustomerDB.Add(new Customer(txtFName.Text, txtLName.Text, txtPhone.Text));
            foreach (Customer C in CustomerDB)
            {
                listBox.Items.Add(C.GetCustomer());
            }
        }
        public Form1()
        {
            InitializeComponent();
            //LoadDB method is called when the form loads
            LoadDB();

            // No need to display any info when opening the form ......  Got to know that after discussion ╮(╯▽╰)╭
            //foreach (Customer C in CustomerDB)
            //{
            //    listBox.DisplayMember = "CustomerDetail";
            //}
            //listBox.DataSource = CustomerDB;
        }
        //When click SEARCH button:
        private void btnSearch_Click(object sender, EventArgs e)
        {
            ClearBoxes();
            // if the textbox is empty...
            if (txtSearch.Text == string.Empty)
            {
                MessageBox.Show("You must enter a customer name.");
                txtSearch.Focus();
            }
            else
            {                
                string[] arrayFName = new string[CustomerDB.Count];
                string[] arrayLName = new string[CustomerDB.Count];
                string[] arrayPhone = new string[CustomerDB.Count];

                for (int i = 0; i < CustomerDB.Count; i++)
                {
                    arrayFName[i] = CustomerDB[i].FName;
                    arrayLName[i] = CustomerDB[i].LName;
                    arrayPhone[i] = CustomerDB[i].Phone;
                }
                // Below is Case-Sensitive search

                // if the data exists in the CustomerDB list...
                for (int i = 0; i < CustomerDB.Count; i++)
                {
                    if (arrayFName[i] == txtSearch.Text || arrayLName[i] == txtSearch.Text || arrayPhone[i] == txtSearch.Text)
                    {
                        txtSearch.Clear();
                        listBox.Items.Clear();
                        Customer item = new Customer(arrayFName[i], arrayLName[i], arrayPhone[i]);

                        listBox.Items.Add(item);

                        listBox.DisplayMember = "CustomerDetail";
                        break;
                    }
                }
                // if the data dose NOT exist in the CustomerDB list...

                if (!(arrayFName.Contains(txtSearch.Text) || arrayLName.Contains(txtSearch.Text) || arrayPhone.Contains(txtSearch.Text)))
                {
                    MessageBox.Show("Customer not found. Please try again.");
                }
            }
        }       
        // When click CLEAR button:
        private void btnClear_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            ClearBoxes();
            txtFName.Focus();
            btnAdd.Enabled = true;    
        }
        // When click CLEAR LIST button:
        private void btnClearList_Click(object sender, EventArgs e)
        {
            ClearDisplay();
            txtSearch.Focus();
        }
        // When click LIST CUSTOMERS button:
        private void btnListCus_Click(object sender, EventArgs e)
        {
            ClearDisplay();
            DisplayCustomers();
        }
        // When click DELETE button:
        private void btnDelete_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            if (listBox.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a customer to delete");
                ClearDisplay();
                DisplayCustomers();
            }
            else
            {
                const string message = "Are you sure you want to delete this customer?";
                const string caption = "Form Closing";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

            // Retrieved from https://msdn.microsoft.com/en-us/library/system.windows.forms.messageboxbuttons(v=vs.110).aspx

                // If NO button was pressed ...
                if (result == DialogResult.No)
                {
                    MessageBox.Show("Operation cancelled.");
                    ClearBoxes();
                }
                // If YES button was pressed ...
                else
                {
                    CustomerDB[listBox.SelectedIndex].FName = null;
                    CustomerDB[listBox.SelectedIndex].LName = null;
                    CustomerDB[listBox.SelectedIndex].Phone = null;
                    string deletedCus = CustomerDB[listBox.SelectedIndex].GetCustomer().ToString();

                    listBox.Items.RemoveAt(listBox.SelectedIndex);
                    listBox.Items.Add(deletedCus);
                    ClearBoxes();

                    MessageBox.Show("The customer has been deleted.");
                }
            }
            btnAdd.Enabled = true;
        }
        // Select an item from the listbox and assign first name, last name and phone to its textbox
        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                txtFName.Text = CustomerDB[listBox.SelectedIndex].FName;
                txtLName.Text = CustomerDB[listBox.SelectedIndex].LName;
                txtPhone.Text = CustomerDB[listBox.SelectedIndex].Phone;
            }
                
        }
        // When click ADD button:
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // if the any of those 3 textboxes are empty...
            if (txtFName.Text == string.Empty || txtLName.Text == string.Empty || txtPhone.Text == string.Empty)
            {
                MessageBox.Show("All textboxes must be filled to continue.");
            }
            // if all those 3 textboxes are filled...
            else
            {
                Customer item1 = new Customer(txtFName.Text, txtLName.Text, txtPhone.Text);
                CustomerDB.Add(item1);              

                ClearBoxes();
                ClearDisplay();

                listBox.Items.Add(item1);
                listBox.DisplayMember = "CustomerDetail";

                MessageBox.Show("New customer has been added.");
            }
        }
        // When click UPDATE button
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;

            // if no item selected...
            if (listBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer to update.");
                ClearDisplay();
                DisplayCustomers();
            }
            // if an item is selected ....
            else
            {
                if (txtFName.Text == string.Empty || txtLName.Text == string.Empty || txtPhone.Text == string.Empty)
                {
                    MessageBox.Show("All textboxes must be filled to continue.");
                }
                else
                {
                    CustomerDB[listBox.SelectedIndex].FName = txtFName.Text;
                    CustomerDB[listBox.SelectedIndex].LName = txtLName.Text;
                    CustomerDB[listBox.SelectedIndex].Phone = txtPhone.Text;
                    string updatedCus = CustomerDB[listBox.SelectedIndex].GetCustomer().ToString();

                    listBox.Items.RemoveAt(listBox.SelectedIndex);
                    listBox.Items.Add(updatedCus);

                    ClearBoxes();
                    ClearDisplay();
                    DisplayCustomers();
                    MessageBox.Show("Customer details updated.");
                }                
            }
            btnAdd.Enabled = true;
        }
        private void txtFName_TextChanged(object sender, EventArgs e)
        {
        }
    }
}



