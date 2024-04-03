using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NguyenTruongGiang
{
    // Tạo class việc cần làm
    class Task_Item
    {
        public string Ten_Viec { get; set; }
        public int Do_Uutien { get; set; }
        public string Mota { get; set; }
        public string TrangThai { get; set; }

        //tạo constructor
        public Task_Item(string taskname, int prior, string descrip)
        {
            Ten_Viec = taskname;
            Do_Uutien = prior;  
            Mota = descrip;
            TrangThai = "Pending";  // trạng thái mặc định
        }


    }
    class Program
    {

        static List<Task_Item> tasks = new List<Task_Item>();
        static void Main(string[] args)
        {
            bool continue_Program = true;
            while (continue_Program)
            {
                Console.WriteLine("-----Cong Viec Can Lam-----");
                Console.WriteLine("1. Khai bao thong tin viec can lam");
                Console.WriteLine("2. Xoa thong tin viec can lam dua vao vi tri");
                Console.WriteLine("3. Cap nhat trang thai dua vao ten viec can lam");
                Console.WriteLine("4. Tim kiem viec can lam dua vao ten hoac do uu tien");
                Console.WriteLine("5. Hien thi danh sach viec can lam theo do uu tien giam dan");
                Console.WriteLine("6. Hien thi toan bo danh sach viec can lam");
                Console.WriteLine("7. Thoat chuong trinh");

                Console.Write("Chon chuc nang: ");
                string chon = Console.ReadLine();

                switch (chon) 
                {
                    case "1":
                        ChucNang1();
                        break;
                    case "2":
                        ChucNang2();
                        break;
                    case "3":
                        ChucNang3();
                        break;
                    case "4":
                        ChucNang4();
                        break;
                    case "5":
                        ChucNang5();
                        break;
                    case "6":
                        ChucNang6();
                        break;
                    case "7":
                        continue_Program = false;   
                        break;
                    default:
                        Console.WriteLine("Lua chon khong hop le! ");
                        break;
                }
            }
        }

        static void ChucNang1() //them viec can lam 
        {
            
            Console.WriteLine("---- Them viec can lam ----");
            Console.Write("Nhap ten viec can lam: ");
            string taskname = Console.ReadLine();

            Console.Write("Nhap do uu tien cong viec (1-5): ");
            int Prior;
            while (!int.TryParse(Console.ReadLine(), out Prior) || Prior <1 || Prior > 5) 
            {
                Console.Write("Do uu tien khong hop le, nhap lai: ");
            
            }

            Console.Write("Nhap mo ta: ");
            string mota = Console.ReadLine();

            Task_Item newTask = new Task_Item(taskname, Prior, mota);
            tasks.Add(newTask);
            Console.WriteLine("Đã them viec '{0}' vao danh sach can lam. ", taskname);


        }

        // xoa việc cần làm theo vị trí mong muốn của người dùng
        static void ChucNang2()
        {
            Console.WriteLine("\n --- Xoa viec can lam ---");

            if (tasks.Count == 0)
            {
                Console.WriteLine("Khong co viec de xoa");
                    return;
            }

            //Hiển thị danh sách các việc cần làm để dễ xóa

            Console.WriteLine("Danh sach cac viec can lam: ");
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i+1}. {tasks[i].Ten_Viec}");
            }

            //Nhap so thu tu viec can xoa

            Console.Write("Nhap so thu tu viec can xoa: ");
            int sothutu;
            while (!int.TryParse(Console.ReadLine(), out sothutu) || sothutu <1 || sothutu > tasks.Count)
            {
                Console.Write("So thu tu ban vua nhap khong hop le! vui long nhap lai: ");
            }

            string taskname = tasks[sothutu - 1].Ten_Viec;
            tasks.RemoveAt(sothutu - 1);
            Console.WriteLine("Da xoa viec '{0}' khoi danh sach. ", taskname);
        }

        //cập nhật trạng thái việc cần làm theo tên việc
        static void ChucNang3() 
        {
            Console.WriteLine("\n Cap nhat trang thai viec can lam ---");

            if (tasks.Count == 0)
            {
                Console.WriteLine("Khong co viec lam can cap nhat");
                return;
            }

            Console.Write("Nhap ten viec can lam de cap nhat trang thai ");
            string taskUpdate = Console.ReadLine();

            Task_Item taskNeedUpdate = tasks.Find(task => task.Ten_Viec.Equals(taskUpdate, StringComparison.OrdinalIgnoreCase));

            if (taskNeedUpdate == null)
            {
                Console.WriteLine("Khong tim thay viec lam co ten '{0}'.", taskUpdate);
            }

            Console.WriteLine("Trang thai hien tai cua '{0}' la: {1}", taskNeedUpdate.Ten_Viec, taskNeedUpdate.TrangThai);
            Console.Write("Nhap trang thai moi: ");
            string trangthaimoi = Console.ReadLine();

            taskNeedUpdate.TrangThai = trangthaimoi;
            Console.WriteLine("Da cap nhat trang thai viec {0} thanh {1}.", taskNeedUpdate.Ten_Viec, taskNeedUpdate.TrangThai);

        }

        //tim kiem viec can lam: 

        static void ChucNang4()
        {
            Console.WriteLine("\n Tim kiem viec can lam --- ");

            if (tasks.Count == 0)
            {
                Console.WriteLine("Khong co viec can lam de tim kiem. ");
                return;
            }

            Console.WriteLine("1. Tim kiem theo TEN viec can lam ");
            Console.WriteLine("2. Tim kiem theo DO UU TIEN ");

            Console.Write("Chon cach tim kiem: ");
            string search = Console.ReadLine();

            switch (search)
            {
                case "1":
                    TimkiemtheoTen();
                    break;
                case "2":
                    TimkiemtheoDoUuTien();
                    break;
                default: 
                    Console.WriteLine("Lua chon khong hop le. ");
                    break;
            }
        }

        // tim kiem theo ten cong viec
        static void TimkiemtheoTen()
        {
            Console.Write("Nhap ten viec can tim: ");
            string Tenviec = Console.ReadLine();

            List <Task_Item> timkiem = tasks.FindAll(task => task.Ten_Viec.Equals(Tenviec, StringComparison.OrdinalIgnoreCase));

            if (timkiem.Count == 0) 
            {
                Console.WriteLine("Khong tim thay viec can lam. ");
                return;
            }
            Console.WriteLine("Cac viec can lam co ten '{0}': ", Tenviec);
            foreach (var task in timkiem)
            {
                Console.WriteLine("Ten: {0}, Do uu tien {1}, Mo ta {2}, Trang thai {3}", task.Ten_Viec,task.Do_Uutien,task.Mota, task.TrangThai);
            }
        } 
        
        //tim kiem theo do uu tien
        static void TimkiemtheoDoUuTien()
        {
            Console.Write("Nhap do uu tien can tim (1-5): ");
            int Douutien;
            while (!int.TryParse(Console.ReadLine(), out Douutien) || Douutien < 1 || Douutien > 5)
            {
                Console.Write("Do uu tien khong hop le, nhap lai: ");

            }
            List<Task_Item> timkiem = tasks.FindAll(task => task.Do_Uutien == Douutien);
            if (timkiem.Count == 0)
            {
                Console.WriteLine("Khong tim thay viec can lam. ");
                return;
            }
            Console.WriteLine("Cac viec can lam co do uu tien '{0}': ", Douutien);
            foreach (var task in timkiem)
            {
                Console.WriteLine("Ten: {0}, Do uu tien {1}, Mo ta {2}, Trang thai {3}", task.Ten_Viec, task.Do_Uutien, task.Mota, task.TrangThai);
            }
        }

        //Hien thi danh sach viec can lam theo do uu tien giam dan
        static void ChucNang5()
        {
            Console.WriteLine("\n Danh sach viec lam da sap xep theo DO UU TIEN giam dan ---");

            if (tasks.Count == 0) 
            {
                Console.WriteLine("Khong co viec can lam. ");
                return;
            }
            List<Task_Item> sort_list = tasks.OrderByDescending(task => task.Do_Uutien).ToList();

            foreach (var task in sort_list)
            {
                Console.WriteLine("Ten viec: {0}, Do uu tien: {1}, Mo ta: {2}, Trang thai: {3}", task.Ten_Viec, task.Do_Uutien, task.Mota, task.TrangThai);
            }
        }

        static void ChucNang6() // Hien thi danh sach viec can lam
        {
            Console.WriteLine("\n --- Danh sach viec can lam ---");
            if (tasks.Count == 0)
            {
                Console.WriteLine("Khong co viec can lam. ");
            }
            else
            {
                foreach (var task in tasks)
                {
                    Console.WriteLine("Ten viec: {0}, Do uu tien: {1}, Mo ta: {2}, Trang thai: {3}", task.Ten_Viec, task.Do_Uutien,task.Mota, task.TrangThai);
                }
            } 
                
        }
    }
}
