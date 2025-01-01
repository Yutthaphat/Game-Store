using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ohh
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string itemChoice = "";
            int itemPrice = 0;
            bool includeBag = false;
            int quantity;
            List<string> orderDetails = new List<string>(); // List to store order details  // array 
            int totalOrderPrice = 0;

            // Welcome message
            Console.WriteLine("    <*Welcome to Game store*>");
            Console.WriteLine("--------------------------");
            Console.Write("Enter your name : ");
            string name = Console.ReadLine();

            string ID;
            while (true)
            {
                Console.Write("Enter your ID : ");
                ID = Console.ReadLine();

                // ตรวจสอบว่า id เป็นตัวเลขเท่านั้น
                if (long.TryParse(ID, out long result) && result >= 0)
                {
                    break; // ออกจากลูปถ้า id เป็นตัวเลข
                }
                else
                {
                    Console.WriteLine("--------------------------");
                    Console.WriteLine("Invalid ID. Please enter numbers only.");
                    Console.WriteLine("--------------------------");
                }
            }
            Console.Clear(); // ลบข้อความก่อนหน้าออก
            Console.WriteLine("    <*Welcome to Game store*>");
            Console.WriteLine("--------------------------");
            Console.WriteLine("Welcome : " + name + " " + ID);

            // Main loop for multiple orders
            while (true)
            {
                // Equipment Selection
                Console.WriteLine("--------------------------");
                Console.WriteLine("Please choose your equipment");
                Console.WriteLine("1. Gun\n2. Sword\n3. Grenade\n4. Exit");
                Console.Write("Enter: ");
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
                {
                    Console.WriteLine("Invalid choice. Please enter numbers only.");
                    continue;
                }


                // Use if-else to handle equipment selection
                if (choice == 1)   // if - else
                {
                    if (!ChooseGun(ref itemChoice, ref itemPrice)) continue; // Call method to choose gun  // ถ้าลบ ! ออกจะไม่ขึ้น error แต่ถ้าเลือก choosegun ก็จะเด้งมาหน้าเลือกใหม่
                                                                             // If you delete !, there will be no error, but if you choose choosegun, it will show to the new selection page.
                }
                else if (choice == 2)
                {
                    if (!ChooseSword(ref itemChoice, ref itemPrice)) continue; // Call method to choose sword
                }
                else if (choice == 3)
                {
                    if (!ChooseGrenade(ref itemChoice, ref itemPrice)) continue; // Call method to choose grenade
                }
                else if (choice == 4)
                {
                    Console.WriteLine("Have a nice day!");
                    break; // Exit the program
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter numbers only.");
                    continue;
                }

                // Enter quantity first
                while (true)
                {
                    Console.WriteLine("----------------------------------");
                    Console.Write("How many pieces do you want?\nEnter: ");
                    string quantityInput = Console.ReadLine();

                    // Try parsing the value and check if it's a positive integer
                    if (int.TryParse(quantityInput, out quantity) && quantity > 0)
                    {
                        break; // Valid value, exit the loop
                    }
                    else
                    {
                        Console.WriteLine("Invalid value. Please enter number.");
                    }
                }

                // Adding the item to the order   // add data input to array
                int itemTotalPrice = itemPrice * quantity;
                bool itemExists = false;
                for (int i = 0; i < orderDetails.Count; i++)
                {
                    if (orderDetails[i].Contains(itemChoice))  // Check if the item already exists
                    {
                        // อัปเดตสินค้าที่มีอยู่แล้วโดยการเพิ่มจำนวน
                        int existingQuantity = int.Parse(orderDetails[i].Split(' ')[0]); // ดึงจำนวนสินค้าปัจจุบันจากรายการคำสั่งซื้อ (extract จำนวนจากข้อความ)
                        existingQuantity += quantity; // เพิ่มจำนวนสินค้าที่ผู้ใช้เลือกซื้อใหม่
                        orderDetails[i] = $"{existingQuantity} {itemChoice} - {itemPrice * existingQuantity} $"; // อัปเดตรายการคำสั่งซื้อด้วยจำนวนและราคาที่คำนวณใหม่
                        totalOrderPrice += itemPrice * quantity; // เพิ่มราคาสินค้าสำหรับจำนวนที่เพิ่มขึ้นในยอดรวมคำสั่งซื้อ
                        itemExists = true; // กำหนดว่าเจอสินค้าที่มีอยู่แล้วในคำสั่งซื้อ
                        break; // ออกจากลูปเมื่อเจอสินค้าที่ต้องการอัปเดต
                    }
                }

                if (!itemExists)  // add data input to array
                {
                    orderDetails.Add($"{quantity} {itemChoice} - {itemTotalPrice} $");
                    totalOrderPrice += itemTotalPrice;
                }

                // Display Current Order Summary ราคารวม
                Console.WriteLine("----------------------------------");
                Console.WriteLine("Current Order Summary:");
                foreach (var item in orderDetails)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine($"Subtotal: {totalOrderPrice} $");

                // Asking if user wants to add more or go to next
                int addMoreChoice;
                while (true)
                {
                    Console.WriteLine("1. Next\n2. Add more");
                    Console.Write("Enter: ");
                    if (int.TryParse(Console.ReadLine(), out addMoreChoice) && (addMoreChoice == 1 || addMoreChoice == 2))
                    {
                        break; // Exit the loop if a valid choice is made
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                        Console.WriteLine("----------------------------------");
                    }
                }

                if (addMoreChoice == 2)
                {
                    continue;
                }

                // Option to add a leather bag
                int bagChoice;
                do
                {
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine("Do you want a Leather bag? (Leather bag = 5 $)");
                    Console.WriteLine("1. Yes\n2. No");
                    Console.Write("Enter: ");
                    string bagInput = Console.ReadLine();

                    if (int.TryParse(bagInput, out bagChoice) && (bagChoice == 1 || bagChoice == 2))
                    {
                        break; // Exit the loop if the input is valid
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                    }
                }
                while (bagChoice != 1 && bagChoice != 2);

                includeBag = (bagChoice == 1);
                int bagPrice = includeBag ? 5 : 0;
                totalOrderPrice += bagPrice;

                // Display Final Order Summary สรุปราคารวม
                Console.WriteLine("----------------------------------");
                Console.WriteLine("Final Order Summary:");
                foreach (var item in orderDetails)
                {
                    Console.WriteLine(item);
                }
                if (includeBag) Console.WriteLine("with Leather bag - 5 $");
                Console.WriteLine($"Total: {totalOrderPrice} $");

                // Confirming the order
                Console.WriteLine("----------------------------------");
                Console.WriteLine("Please confirm your order");
                int confirmChoice = 0;
                while (true)
                {
                    Console.WriteLine("1. Confirm\n2. No");
                    Console.Write("Enter: ");
                    string input = Console.ReadLine();

                    // Check if the input is either 1 or 2
                    if (int.TryParse(input, out confirmChoice) && (confirmChoice == 1 || confirmChoice == 2))
                    {
                        break; // Exit the loop if a valid choice is made
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                    }
                }

                if (confirmChoice == 1)
                {
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine("Your total is: " + totalOrderPrice + " $");

                    while (true)
                    {
                        Console.Write("Enter the amount you will pay: ");
                        string paymentInput = Console.ReadLine();
                        if (int.TryParse(paymentInput, out int paymentAmount))
                        {
                            if (paymentAmount >= totalOrderPrice)
                            {
                                int change = paymentAmount - totalOrderPrice;
                                Console.WriteLine("----------------------------------");
                                Console.WriteLine("Payment successful!");
                                Console.WriteLine("Your change is: " + change + " $");
                                Console.WriteLine("----------------------------------");
                                Console.WriteLine("Thank you for shopping!");
                                break; // Exit the loop if payment is successful
                            }
                            else
                            {
                                Console.WriteLine("Insufficient payment. Please enter an amount equal to or greater than the total.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input Please enter a valid number.");
                        }
                    }

                    // Clear order details after completion
                    orderDetails.Clear();
                    totalOrderPrice = 0;  // Reset total price

                    // Ask if the user wants to continue shopping after confirmation
                    string continueShopping;
                    while (true)
                    {
                        Console.WriteLine("----------------------------------");
                        Console.WriteLine("Do you want to continue shopping? (yes/no)");
                        Console.Write("Enter: ");
                        continueShopping = Console.ReadLine().ToLower();

                        if (continueShopping == "yes" || continueShopping == "no")
                        {
                            break; // Exit the loop if a valid choice is made
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                        }
                    }

                    if (continueShopping == "no")
                    {
                        Console.WriteLine("--------------------------");
                        Console.WriteLine("Have a nice day!");
                        break;
                    }
                    else
                    {
                        Console.Clear(); // Clear the screen before returning to the store
                        Console.WriteLine("    <*Welcome to Game store*>");
                        Console.WriteLine("--------------------------");
                        Console.WriteLine("Welcome : " + name + " " + ID);
                    }
                }
                else if (confirmChoice == 2)
                {
                    totalOrderPrice -= bagPrice;
                }
            }

        }

        // Method to choose gun
        static bool ChooseGun(ref string itemChoice, ref int itemPrice)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Choose your Gun type");
            Console.WriteLine("1. Pistol (20 $)");
            Console.WriteLine("2. Submachine gun (30 $)");
            Console.WriteLine("3. Shotgun (40 $)");
            Console.WriteLine("4. Sniper rifle (500 $)");
            Console.WriteLine("5. Back");
            Console.Write("Enter: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 5)
            {
                Console.WriteLine("Invalid choice. Please enter numbers only.");
                return false;
            }

            switch (choice)  // switch case 
            {
                case 1:
                    itemChoice = "Pistol";
                    itemPrice = 20;
                    return true;
                case 2:
                    itemChoice = "Submachine gun";
                    itemPrice = 30;
                    return true;
                case 3:
                    itemChoice = "Shotgun";
                    itemPrice = 40;
                    return true;
                case 4:
                    itemChoice = "Sniper rifle";
                    itemPrice = 500;
                    return true;
                case 5:
                    Console.WriteLine("Returning to equipment selection menu.");
                    return false;
            }
            return false;
        }

        // Method to choose sword
        static bool ChooseSword(ref string itemChoice, ref int itemPrice)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Choose your Sword type");
            Console.WriteLine("1. Rapier (35 $)");
            Console.WriteLine("2. Arming Sword (40 $)");
            Console.WriteLine("3. Long sword (45 $)");
            Console.WriteLine("4. Katana (70 $)");
            Console.WriteLine("5. Back");
            Console.Write("Enter: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 5)
            {
                Console.WriteLine("Invalid choice. Please enter numbers only.");
                return false;
            }

            // Use switch case for sword type selection
            switch (choice)
            {
                case 1:
                    itemChoice = "Rapier";
                    itemPrice = 35;
                    return true;
                case 2:
                    itemChoice = "Arming Sword";
                    itemPrice = 40;
                    return true;
                case 3:
                    itemChoice = "Long sword";
                    itemPrice = 45;
                    return true;
                case 4:
                    itemChoice = "Katana";
                    itemPrice = 70;
                    return true;
                case 5:
                    Console.WriteLine("Returning to equipment selection menu.");
                    return false;
            }
            return false;
        }

        // Method to choose grenade
        static bool ChooseGrenade(ref string itemChoice, ref int itemPrice)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Choose your Grenade type");
            Console.WriteLine("1. Flashbang (15 $)");
            Console.WriteLine("2. Smoke Grenade (25 $)");
            Console.WriteLine("3. Fragmentation Grenade (50 $)");
            Console.WriteLine("4. Back");
            Console.Write("Enter: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 4)
            {
                Console.WriteLine("Invalid choice. Please enter numbers only.");
                return false;
            }

            // Use switch case for grenade type selection 
            switch (choice)
            {
                case 1:
                    itemChoice = "Flashbang";
                    itemPrice = 15;
                    return true;
                case 2:
                    itemChoice = "Smoke Grenade";
                    itemPrice = 25;
                    return true;
                case 3:
                    itemChoice = "Fragmentation Grenade";
                    itemPrice = 50;
                    return true;
                case 4:
                    Console.WriteLine("Returning to equipment selection menu.");
                    return false; // กำหนดค่าเป็น false เพื่อระบุว่าไม่มีการเลือกระเบิด

            }
            return false;
        }
    }
}