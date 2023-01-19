namespace WinFormsTest2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        private void button3_Click(object sender, EventArgs e)
        {
            string numbers = "";
            int num1 = int.Parse(textBoxNum1.Text);
            int num2 = int.Parse(textBoxNum2.Text);
            // num1 부터 num2 개의 숫자를 더함 
            foreach (var num in Enumerable.Range(num1, num2))
            {

                numbers += $"{num}";
                if (num != num1 + num2 - 1)
                {
                    numbers += $"{textBoxSeperator.Text}";
                }
            }


            //MessageBox.Show($"실행!\n {numbers}mm");
            textBoxOutput.Text = numbers;

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void labelDesc_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}