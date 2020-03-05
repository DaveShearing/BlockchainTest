using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace BlockchainTest
{
    class Program
    {
        


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            SHA256 algo = SHA256.Create();

            BlockChain newChain = new BlockChain();
            newChain.blocks = new List<Block>();

            string data = "eee";

            byte[] bytes = algo.ComputeHash(Encoding.UTF8.GetBytes(data));

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            Console.WriteLine(builder.ToString());

            //int e = GetSolution();

            //Console.WriteLine(e);

            while (newChain.blocks.Count < 3)
            {
                newChain.blocks.Add(CreateBlock());
            }

            foreach (Block block in newChain.blocks)
            {
                Console.WriteLine(block.proof);
                Console.WriteLine(block.timeStamp);
            }

            Console.WriteLine(newChain.blocks[0].timeStamp);
        }

        public static Block CreateBlock ()
        {
            Block newBlock = new Block();

            newBlock.blockNo = 1;
            newBlock.timeStamp = DateTime.Now;
            newBlock.proof = GetSolution();
            
            

            return newBlock;
        }

        public static int GetSolution ()
        {
            Random random = new Random();
            

            int x = random.Next();
            int y = 0;
            int z = 0;

            int diff = 2;

            byte[] bytes;
            byte[] section = new byte[diff];
            byte[] target = { 0, 0 };

            

            SHA256 algo = SHA256.Create();

            bool solved = false;

            StringBuilder builder = new StringBuilder();

            while (solved == false)
            {
                z = x * y;

                bytes = algo.ComputeHash(Encoding.UTF8.GetBytes(z.ToString()));

                
                Array.Copy(bytes, section, diff);

                if (StructuralComparisons.StructuralEqualityComparer.Equals(section,target))
                {
                    solved = true;
                    break;
                } else
                {
                    
                    y++;
                }

            }

            bytes = algo.ComputeHash(Encoding.UTF8.GetBytes(z.ToString()));

            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            Console.WriteLine(builder.ToString());

            

            return y;
        }

        private int GetHash()
        {


            return 0;
        }
    }

    public class BlockChain
    {
        public List<Block> blocks;


    }

    public class Block
    {
        public int blockNo;
        public DateTime timeStamp;
        public Transaction[] transactions;
        public int proof;
        public string previousHash;
    }

    public class Transaction
    {
        string sender;
        string recipient;
        float amount;
    }

}
