using System;
using System.Globalization;



namespace Validador
{

    class programa
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite um CPF ou CNPJ para validar:");
            string num = Console.ReadLine();
            string formataNumero = "";

            if (num.Length == 11)
            {
                formataNumero = FormatCPF(num);
                if (IsValidCPF(num))
                {
                    Console.WriteLine("CPF válido: " + formataNumero);
                }
                else
                {
                    Console.WriteLine("CPF inválido!");
                }
            }
            else if (num.Length == 14)
            {
                formataNumero = FormatCNPJ(num);
                if (IsValidCNPJ(num))
                {
                    Console.WriteLine("CNPJ válido: " + formataNumero);
                }
                else
                {
                    Console.WriteLine("CNPJ inválido!");
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida! O CPF possui 11 dígitos e o CNPJ possui 14 dígitos.");
            }

            Console.ReadLine();
        }

        private static string FormatCPF(string cpf)
        {
            return string.Format("{0:000\\.000\\.000\\-00}", long.Parse(cpf));
        }

        private static string FormatCNPJ(string cnpj)
        {
            return string.Format("{0:00\\.000\\.000\\/0000\\-00}", long.Parse(cnpj));
        }

        // Implementação da validação de CPF
        private static bool IsValidCPF(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (!long.TryParse(cpf, out long parsedCpf))
            {
                return false;
            }

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
            {
                numeros[i] = int.Parse(cpf[i].ToString());
            }

            int soma = 0;
            int mod;

            for (int i = 0; i < 9; i++)
            {
                soma += numeros[i] * (10 - i);
            }

            mod = soma % 11;
            if (mod < 2)
            {
                if (numeros[9] != 0)
                {
                    return false;
                }
            }
            else
            {
                if (numeros[9] != 11 - mod)
                {
                    return false;
                }
            }

            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += numeros[i] * (11 - i);
            }

            mod = soma % 11;
            if (mod < 2)
            {
                if (numeros[10] != 0)
                {
                    return false;
                }
            }
            else
            {
                if (numeros[10] != 11 - mod)
                {
                    return false;
                }
            }

            return true;
        }

        // Implementação da validação de CNPJ       
        private static bool IsValidCNPJ(string cnpj)
        {
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14 || !long.TryParse(cnpj, out long parsedCnpj))
            {
                return false;
            }

            int[] numeros = new int[14];

            for (int i = 0; i < 14; i++)
            {
                numeros[i] = int.Parse(cnpj[i].ToString());
            }

            int soma = 0;
            int mod;
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            // Cálculo do primeiro dígito verificador
            for (int i = 0; i < 12; i++)
            {
                soma += numeros[i] * multiplicador1[i];
            }

            mod = soma % 11;
            int digito1 = mod < 2 ? 0 : 11 - mod;

            if (numeros[12] != digito1)
            {
                return false;
            }

            soma = 0;

            // Cálculo do segundo dígito verificador
            for (int i = 0; i < 13; i++)
            {
                soma += numeros[i] * multiplicador2[i];
            }

            mod = soma % 11;
            int digito2 = mod < 2 ? 0 : 11 - mod;

            if (numeros[13] != digito2)
            {
                return false;
            }

            return true;
        }

    }

}
