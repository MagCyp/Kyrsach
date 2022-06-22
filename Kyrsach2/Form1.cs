using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System;

namespace Kyrsach2
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        private readonly FirstEntities1 db;
        public Form1()
        {
            InitializeComponent();
            db = new FirstEntities1();
            
        }



        private void Form1_Load(object sender, System.EventArgs e)
        {
            RefreshForm();
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            RefreshForm();
        }
        
        private void RefreshForm()
        {
            bookDataGridView.DataSource = db.Book.ToList();
            disciplineDataGridView.DataSource = db.Discipline.ToList();
            issues_of_booksDataGridView.DataSource = db.Issues_of_books.ToList();
            groupsDataGridView.DataSource = db.Groups.ToList();
            teacherDataGridView.DataSource = db.Teacher.ToList();
            studentDataGridView.DataSource = db.Student.ToList();
            personDataGridView.DataSource = db.Person.ToList();
            student_gradeDataGridView.DataSource = db.Student_grade.ToList();

        }

        //table 1 
        private void button1_Click(object sender, System.EventArgs e)
        {
            try
            {
                AddBook(GetBookCount());
                RefreshForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int GetBookCount()
        {
            return db.Book.Count() > 0 ? db.Book.Max(x => x.book_ID):0;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }


        private void AddBook(int count)
        {

                textBox1.Text = (GetBookCount() + 1).ToString();
                textBox1.ReadOnly = true;
                if (!ValidationBox1())
                {

                    var book = new Book
                    {
                        book_ID = ++count,
                        title = textBox2.Text,
                        author = textBox3.Text,
                        genre = textBox4.Text,
                        price = decimal.Parse(textBox5.Text),
                        book_count = int.Parse(textBox6.Text)
                    };
                    db.Book.Add(book);
                    db.SaveChanges();
                }
                else
                    throw new InvalidOperationException("Enter exception");

        }
        private bool ValidationBox1()
        {
            return  string.IsNullOrEmpty(textBox2.Text) && string.IsNullOrEmpty(textBox3.Text) && string.IsNullOrEmpty(textBox4.Text) && string.IsNullOrEmpty(textBox5.Text) && string.IsNullOrEmpty(textBox6.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (bookDataGridView.SelectedRows.Count > 0)
                {

                    var id = int.Parse(bookDataGridView.SelectedRows[0].Cells[0].Value.ToString());
                    var book = db.Book.FirstOrDefault(x => x.book_ID == id);
                    db.Book.Remove(book);
                    db.SaveChanges();
                    RefreshForm();
                }
                else
                    MessageBox.Show("Select rows");
            }
            catch(Exception)
            {
                MessageBox.Show("You can`t delite cell");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            button26.Visible = true;
            var id = int.Parse(bookDataGridView.SelectedRows[0].Cells[0].Value.ToString());
            var book = db.Book.FirstOrDefault(x => x.book_ID == id);
            textBox1.Text = id.ToString();
            textBox2.Text = book.title;
            textBox3.Text = book.author;
            textBox4.Text = book.genre;
            textBox5.Text = book.price.ToString();
            textBox6.Text = book.book_count.ToString();

        }
        private void button26_Click(object sender, EventArgs e)
        {
            if (!ValidationBox1())
            {
                var id = int.Parse(bookDataGridView.SelectedRows[0].Cells[0].Value.ToString());
                var book = db.Book.FirstOrDefault(x => x.book_ID == id);

                book.title = textBox2.Text;
                book.author = textBox3.Text;
                book.genre = textBox4.Text;
                book.price = decimal.Parse(textBox5.Text);
                book.book_count = int.Parse(textBox6.Text);
                db.SaveChanges();
                RefreshForm();
                button26.Visible = false;
            }
            else
                MessageBox.Show("ex");

        }

        //table 2
        private int GetDisciplineCount()
        {
            return db.Discipline.Count() > 0 ? db.Discipline.Max(x => x.discipline_ID) : 0;
        }

        private void textBox8_TextChanged_1(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox7.Text = (GetDisciplineCount() + 1).ToString();
            textBox7.ReadOnly = true;
            try
            {
                AddDiscipline(GetDisciplineCount());
                RefreshForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddDiscipline(int discount)
        {

            if (!string.IsNullOrEmpty(textBox8.Text))
            {
                var discipline = new Discipline
                {

                    discipline_ID = ++discount,
                    name = textBox8.Text
                };
                db.Discipline.Add(discipline);
                db.SaveChanges();
            }
            else
                throw new InvalidOperationException("Enter exeption");

        }
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (disciplineDataGridView.SelectedRows.Count > 0)
                {

                    var id = int.Parse(disciplineDataGridView.SelectedRows[0].Cells[0].Value.ToString());
                    var discipline = db.Discipline.FirstOrDefault(x => x.discipline_ID == id);
                    db.Discipline.Remove(discipline);
                    db.SaveChanges();
                    RefreshForm();
                }
                else
                    MessageBox.Show("Select rows");
            }
            catch (Exception)
            {
                MessageBox.Show("You can`t delite cell");
            }
        }
        private void button27_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox8.Text))
            {
                var id = int.Parse(disciplineDataGridView.SelectedRows[0].Cells[0].Value.ToString());
                var discipline = db.Discipline.FirstOrDefault(x => x.discipline_ID == id);
                discipline.name = textBox8.Text;
                db.SaveChanges();
                RefreshForm();
                button27.Visible = false;
            }
            else
                MessageBox.Show("ex");
        }
        private void button5_Click(object sender, EventArgs e)
        {
            button27.Visible = true;
            var id = int.Parse(disciplineDataGridView.SelectedRows[0].Cells[0].Value.ToString());
            var discipline = db.Discipline.FirstOrDefault(x => x.discipline_ID == id);
            textBox7.Text = id.ToString();
            textBox8.Text = discipline.name;
        }
        //table 3

        private void button10_Click(object sender, EventArgs e)
        {
            textBox9.Text = (GetIssueCount() + 1).ToString();
            textBox9.ReadOnly = true;
            try
            {
                AddIssue(GetIssueCount());
                RefreshForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int GetIssueCount()
        {
            return db.Issues_of_books.Count() > 0 ? db.Issues_of_books.Max(x => x.issues_of_books_ID): 0;
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void AddIssue(int Issuecount)
        {

                if (!ValidationBox2())
                {
                    var issue = new Issues_of_books
                    {

                        issues_of_books_ID = ++Issuecount,
                        date_of_issues = Convert.ToDateTime(textBox10.Text),
                        date_of_comeback = Convert.ToDateTime(textBox11.Text),
                        book_ID = IsBookID(Convert.ToInt32(textBox12.Text))?Convert.ToInt32(textBox12.Text): throw new InvalidOperationException("ID ex"),
                        student_ID = IsStudentID(Convert.ToInt32(textBox13.Text))?Convert.ToInt32(textBox13.Text):throw new InvalidOperationException("ID ex")
                    };
                    db.Issues_of_books.Add(issue);
                    db.SaveChanges();
                }
                else
                    throw new InvalidOperationException("Enter exeption");
            

        }

        private bool ValidationBox2()
        {
            return string.IsNullOrEmpty(textBox10.Text) && string.IsNullOrEmpty(textBox11.Text) && string.IsNullOrEmpty(textBox12.Text) && string.IsNullOrEmpty(textBox13.Text);
        }

        private void button9_Click(object sender, EventArgs e)
        {
                try
                {
                    if (issues_of_booksDataGridView.SelectedRows.Count > 0)
                    {

                        var id = int.Parse(issues_of_booksDataGridView.SelectedRows[0].Cells[0].Value.ToString());
                        var issue = db.Issues_of_books.FirstOrDefault(x => x.issues_of_books_ID == id);
                        db.Issues_of_books.Remove(issue);
                        db.SaveChanges();
                        RefreshForm();
                    }
                    else
                        MessageBox.Show("Select rows");
                }
                catch (Exception)
                {
                    MessageBox.Show("You can`t delite cell");
                }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            button28.Visible = true;
            var id = int.Parse(issues_of_booksDataGridView.SelectedRows[0].Cells[0].Value.ToString());
            var issue = db.Issues_of_books.FirstOrDefault(x => x.issues_of_books_ID == id);
            textBox9.Text = id.ToString();
            textBox10.Text = issue.date_of_issues.ToString();
            textBox11.Text = issue.date_of_comeback.ToString();
            textBox12.Text = issue.book_ID.ToString();
            textBox13.Text = issue.student_ID.ToString();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (!ValidationBox2())
            {
                var id = int.Parse(issues_of_booksDataGridView.SelectedRows[0].Cells[0].Value.ToString());
                var issue = db.Issues_of_books.FirstOrDefault(x => x.issues_of_books_ID == id);
                issue.date_of_issues = Convert.ToDateTime(textBox10.Text);
                issue.date_of_comeback = Convert.ToDateTime(textBox11.Text);
                issue.book_ID = Convert.ToInt32(textBox12.Text);
                issue.student_ID = Convert.ToInt32(textBox13.Text);
                db.SaveChanges();
                RefreshForm();
                button28.Visible = false;
            }
            else
                MessageBox.Show("ex");
        }

        //table 4
        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            
        }

        private int GetGroupCount()
        {
            return db.Groups.Count() > 0 ? db.Groups.Max(x => x.group_ID) : 0;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox14.Text = (GetGroupCount() + 1).ToString();
            textBox14.ReadOnly = true;
            try
            {
                AddGroup(GetGroupCount());
                RefreshForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddGroup(int gcount)
        {

                if (!string.IsNullOrEmpty(textBox15.Text))
                {
                    var group = new Groups
                    {

                        group_ID = ++gcount,
                        group_name = textBox15.Text
                    };
                    db.Groups.Add(group);
                    db.SaveChanges();
                }
                else
                    throw new InvalidOperationException("Enter exeption");

        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                if (groupsDataGridView.SelectedRows.Count > 0)
                {

                    var id = int.Parse(groupsDataGridView.SelectedRows[0].Cells[0].Value.ToString());
                    var group = db.Groups.FirstOrDefault(x => x.group_ID == id);
                    db.Groups.Remove(group);
                    db.SaveChanges();
                    RefreshForm();
                }
                else
                    MessageBox.Show("Select rows");
            }
            catch (Exception)
            {
                MessageBox.Show("You can`t delite cell");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            button29.Visible = true;
            var id = int.Parse(groupsDataGridView.SelectedRows[0].Cells[0].Value.ToString());
            var group = db.Groups.FirstOrDefault(x => x.group_ID == id);
            textBox14.Text = id.ToString();
            textBox15.Text = group.group_name;

        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox15.Text))
            {
                var id = int.Parse(groupsDataGridView.SelectedRows[0].Cells[0].Value.ToString());
                var group = db.Groups.FirstOrDefault(x => x.group_ID == id);
                group.group_name = textBox15.Text;
                db.SaveChanges();
                RefreshForm();
                button29.Visible = false;
            }
            else
                MessageBox.Show("ex");
        }

        //table 5
        private void textBox17_TextChanged(object sender, EventArgs e)
        {
        }

        private int GetTeacherCount()
        {
            return db.Teacher.Count() > 0 ? db.Teacher.Max(x => x.passport_ID) : 0;
        }
        private void button16_Click(object sender, EventArgs e)
        {
            textBox16.Text = (GetTeacherCount() + 1).ToString();
            textBox16.ReadOnly = true;

            try
            {
                AddTeacher(GetTeacherCount());
                RefreshForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddTeacher(int tcount)
        {

                if (!ValidationBox3())
                {
                    var teacher = new Teacher
                    {
                        passport_ID = ++tcount,
                        name = textBox17.Text,
                        surname = textBox18.Text,
                        scientist_degree = textBox19.Text
                    };
                    db.Teacher.Add(teacher);
                    db.SaveChanges();
                }
                else
                    throw new InvalidOperationException("Enter exeption");

        }
        private bool ValidationBox3()
        {
            return string.IsNullOrEmpty(textBox17.Text) && string.IsNullOrEmpty(textBox18.Text) && string.IsNullOrEmpty(textBox19.Text);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                if (teacherDataGridView.SelectedRows.Count > 0)
                {

                    var id = int.Parse(teacherDataGridView.SelectedRows[0].Cells[0].Value.ToString());
                    var teacher = db.Teacher.FirstOrDefault(x => x.passport_ID == id);
                    db.Teacher.Remove(teacher);
                    db.SaveChanges();
                    RefreshForm();
                }
                else
                    MessageBox.Show("Select rows");
            }
            catch (Exception)
            {
                MessageBox.Show("You can`t delite cell");
            }
        }
        private void button30_Click(object sender, EventArgs e)
        {

                if (!ValidationBox3())
                {
                    var id = int.Parse(teacherDataGridView.SelectedRows[0].Cells[0].Value.ToString());
                    var teacher = db.Teacher.FirstOrDefault(x => x.passport_ID == id);
                    teacher.name = textBox17.Text;
                    teacher.surname = textBox18.Text;
                    teacher.scientist_degree = textBox19.Text;
                    db.SaveChanges();
                    RefreshForm();
                    button30.Visible = false;
                }
                else
                    MessageBox.Show("ex");

        }

        private void button14_Click(object sender, EventArgs e)
        {
            button30.Visible = true;
            var id = int.Parse(teacherDataGridView.SelectedRows[0].Cells[0].Value.ToString());
            var teacher = db.Teacher.FirstOrDefault(x => x.passport_ID == id);
            textBox16.Text = id.ToString();
            textBox17.Text = teacher.name;
            textBox18.Text = teacher.surname;
            textBox19.Text = teacher.scientist_degree;
        }


        //table 6
        private void button19_Click(object sender, EventArgs e)
        {
            textBox21.Text = (GetStudentCount() + 1).ToString();
            textBox21.ReadOnly = true;
            try
            {
                AddStudent(GetStudentCount());
                RefreshForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddStudent(int scount)
        {

                if (!ValidationBox4())
                {
                    var student = new Student
                    {
                        passport_ID = IsPassportID(Convert.ToInt32(textBox20.Text))?Convert.ToInt32(textBox20.Text):throw new InvalidOperationException("ID ex"),
                        student_ID = ++scount,
                        group_ID = IsGroupID(Convert.ToInt32(textBox22.Text))?Convert.ToInt32(textBox22.Text):throw new InvalidOperationException("ID ex"),
                        scholarship = Convert.ToDecimal(textBox23.Text),
                        place_in_rating = Convert.ToInt32(textBox24.Text),
                        dorm_room = Convert.ToInt32(textBox25.Text),
                        headmaster = Convert.ToBoolean(textBox26.Text)
                    };
                    db.Student.Add(student);
                    db.SaveChanges();
                }
                else
                    throw new InvalidOperationException("Enter exeption");

        }
        private bool ValidationBox4()
        {
            return string.IsNullOrEmpty(textBox20.Text) && string.IsNullOrEmpty(textBox22.Text) && string.IsNullOrEmpty(textBox23.Text) && string.IsNullOrEmpty(textBox24.Text) && string.IsNullOrEmpty(textBox25.Text) && string.IsNullOrEmpty(textBox26.Text);
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {


        }

        private int GetStudentCount()
        {
            return db.Student.Count() > 0 ? db.Student.Max(x => x.student_ID) : 0;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                if (studentDataGridView.SelectedRows.Count > 0)
                {

                    var id = int.Parse(studentDataGridView.SelectedRows[0].Cells[0].Value.ToString());
                    var student = db.Student.FirstOrDefault(x => x.student_ID == id);
                    db.Student.Remove(student);
                    db.SaveChanges();
                    RefreshForm();
                }
                else
                    MessageBox.Show("Select rows");
            }
            catch (Exception)
            {
                MessageBox.Show("You can`t delite cell");
            }
        }
        private void button17_Click(object sender, EventArgs e)
        {
            button31.Visible = true;
            var id = int.Parse(studentDataGridView.SelectedRows[0].Cells[0].Value.ToString());
            var student = db.Student.FirstOrDefault(x => x.student_ID == id);
            textBox20.Text = student.passport_ID.ToString();
            textBox21.Text = id.ToString();
            textBox22.Text = student.group_ID.ToString();
            textBox23.Text = student.scholarship.ToString();
            textBox24.Text = student.place_in_rating.ToString();
            textBox25.Text = student.dorm_room.ToString();
            textBox26.Text = student.headmaster.ToString();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (!ValidationBox4())
            {
                var id = int.Parse(studentDataGridView.SelectedRows[0].Cells[0].Value.ToString());
                var student = db.Student.FirstOrDefault(x => x.student_ID == id);
                student.passport_ID = Convert.ToInt32(textBox20.Text);
                student.group_ID = Convert.ToInt32(textBox22.Text);
                student.scholarship = Convert.ToDecimal(textBox23.Text);
                student.place_in_rating = Convert.ToInt32(textBox24.Text);
                student.dorm_room = Convert.ToInt32(textBox25.Text);
                student.headmaster = Convert.ToBoolean(textBox26.Text);
                db.SaveChanges();
                RefreshForm();
                button31.Visible = false;
            }
            else
                MessageBox.Show("ex");
        }

        //table 7
        private void textBox28_TextChanged(object sender, EventArgs e)
        {

        }

        private int GetPersonCount()
        {
            return db.Person.Count() > 0 ? db.Person.Max(x => x.passport_ID) : 0;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            textBox27.Text = (GetPersonCount() + 1).ToString();
            textBox27.ReadOnly = true;
            try
            {
                AddPerson(GetPersonCount());
                RefreshForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddPerson(int pcount)
        {

                if (!ValidationBox5())
                {
                    var person = new Person
                    {
                        passport_ID = ++pcount,
                        identification_code = Convert.ToInt32(textBox28.Text),
                        name = textBox29.Text,
                        surname = textBox30.Text,
                        Year_of_birth = Convert.ToDateTime(textBox31.Text),
                        Place_of_birth = textBox32.Text,
                        adress = textBox33.Text,
                        sex = textBox34.Text,
                        marital_status = Convert.ToBoolean(textBox35.Text)
                    };
                    db.Person.Add(person);
                    db.SaveChanges();
                }
                else
                    throw new InvalidOperationException("Enter exeption");

        }
        private bool ValidationBox5() 
        {
            return string.IsNullOrEmpty(textBox28.Text) && string.IsNullOrEmpty(textBox29.Text) && string.IsNullOrEmpty(textBox30.Text) && string.IsNullOrEmpty(textBox31.Text) && string.IsNullOrEmpty(textBox32.Text) && string.IsNullOrEmpty(textBox33.Text) && string.IsNullOrEmpty(textBox34.Text) && string.IsNullOrEmpty(textBox35.Text);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                if (personDataGridView.SelectedRows.Count > 0)
                {

                    var id = int.Parse(personDataGridView.SelectedRows[0].Cells[0].Value.ToString());
                    var person = db.Person.FirstOrDefault(x => x.passport_ID == id);
                    db.Person.Remove(person);
                    db.SaveChanges();
                    RefreshForm();
                }
                else
                    MessageBox.Show("Select rows");
            }
            catch (Exception)
            {
                MessageBox.Show("You can`t delite cell");
            }
        }
        private void button20_Click(object sender, EventArgs e)
        {
            button32.Visible = true;
            var id = int.Parse(personDataGridView.SelectedRows[0].Cells[0].Value.ToString());
            var person = db.Person.FirstOrDefault(x => x.passport_ID == id);
            textBox27.Text = id.ToString();
            textBox28.Text = person.identification_code.ToString();
            textBox29.Text = person.name;
            textBox30.Text = person.surname;
            textBox31.Text = person.Year_of_birth.ToString();
            textBox32.Text = person.Place_of_birth;
            textBox33.Text = person.adress;
            textBox34.Text = person.sex;
            textBox35.Text = person.marital_status.ToString();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (!ValidationBox5())
            {
                var id = int.Parse(personDataGridView.SelectedRows[0].Cells[0].Value.ToString());
                var person = db.Person.FirstOrDefault(x => x.passport_ID == id);
                person.identification_code = Convert.ToInt32(textBox28.Text);
                person.name = textBox29.Text;
                person.surname = textBox30.Text;
                person.Year_of_birth = Convert.ToDateTime(textBox31.Text);
                person.Place_of_birth = textBox32.Text;
                person.adress = textBox33.Text;
                person.sex = textBox34.Text;
                person.marital_status = Convert.ToBoolean(textBox35.Text);
                db.SaveChanges();
                RefreshForm();
                button32.Visible = false;
            }
            else
                MessageBox.Show("ex");
        }

        //table 8
        private void button25_Click(object sender, EventArgs e)
        {
            textBox36.Text = (GetPersonCount() + 1).ToString();
            textBox36.ReadOnly = true;
            try
            {
                AddGrade(GetnumCount());
                RefreshForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int GetnumCount()
        {
            return db.Student_grade.Count() > 0 ? db.Student_grade.Max(x => x.number) : 0;
        }
        private void AddGrade(int numb)
        {

                if (!ValidationBox6())
                {
                    var student_grade = new Student_grade
                    {
                        number = ++numb,
                        dsscipline_ID = IsDisciplineID(Convert.ToInt32(textBox37.Text))?Convert.ToInt32(textBox37.Text):throw new InvalidOperationException("ID ex"),
                        student_ID = IsStudentID(Convert.ToInt32(textBox38.Text))?Convert.ToInt32(textBox38.Text):throw new InvalidOperationException("ID ex"),
                        grade = Convert.ToDouble(textBox39.Text)
                    };
                    db.Student_grade.Add(student_grade);
                    db.SaveChanges();
                }
                else
                    throw new InvalidOperationException("Enter exeption");

        }
        private bool ValidationBox6()
        {
            return string.IsNullOrEmpty(textBox36.Text) && string.IsNullOrEmpty(textBox37.Text) && string.IsNullOrEmpty(textBox38.Text) && string.IsNullOrEmpty(textBox39.Text);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            try
            {
                if (student_gradeDataGridView.SelectedRows.Count > 0)
                {

                    var id = int.Parse(student_gradeDataGridView.SelectedRows[0].Cells[0].Value.ToString());
                    var student_grade = db.Student_grade.FirstOrDefault(x => x.number== id);
                    db.Student_grade.Remove(student_grade);
                    db.SaveChanges();
                    RefreshForm();
                }
                else
                    MessageBox.Show("Select rows");
            }
            catch (Exception)
            {
                MessageBox.Show("You can`t delite cell");
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            button33.Visible = true;
            var id = int.Parse(student_gradeDataGridView.SelectedRows[0].Cells[0].Value.ToString());
            var student_grade = db.Student_grade.FirstOrDefault(x => x.number == id);
            textBox36.Text = id.ToString();
            textBox37.Text = student_grade.dsscipline_ID.ToString();
            textBox38.Text = student_grade.student_ID.ToString();
            textBox39.Text = student_grade.grade.ToString();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (!ValidationBox6())
            {
                var id = int.Parse(student_gradeDataGridView.SelectedRows[0].Cells[0].Value.ToString());
                var student_grade = db.Student_grade.FirstOrDefault(x => x.number == id);
                student_grade.dsscipline_ID = Convert.ToInt32(textBox37.Text);
                student_grade.student_ID = Convert.ToInt32(textBox38.Text);
                student_grade.grade = Convert.ToDouble(textBox39.Text);
                db.SaveChanges();
                RefreshForm();
                button33.Visible = false;
            }
            else
                MessageBox.Show("ex");
        }

        private bool IsBookID(int id)
        {
            return db.Book.Any(x=>x.book_ID==id);
                        
        }
        private bool IsPassportID(int id)
        {
            return db.Person.Any(x => x.passport_ID == id);
        }
        private bool IsGroupID(int id)
        {
            return db.Groups.Any(x => x.group_ID == id);
        }
        private bool IsStudentID(int id)
        {
            return db.Student.Any(x => x.student_ID == id);
        }
        private bool IsDisciplineID(int id)
        {
            return db.Discipline.Any(x => x.discipline_ID == id);
        }
    }
}

