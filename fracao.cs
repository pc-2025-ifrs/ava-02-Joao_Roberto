using System;
using System.Globalization;

public class Fracao : IEquatable<Fracao>
{
    
    public int Numerador { get; private set; }
    public int Denominador { get; private set; }

    #region Construtores

   
    public Fracao(int numerador, int denominador)
    {
        
        ArgumentOutOfRangeException.ThrowIfZero(denominador, nameof(denominador));

        Numerador = numerador;
        Denominador = denominador;
        Simplificar();
    }

   
    public Fracao(int inteiro) : this(inteiro, 1) { }

 
    public Fracao(string fracaoString)
    {
        string[] partes = fracaoString.Split('/');
        if (partes.Length != 2 || !int.TryParse(partes[0], out int num) || !int.TryParse(partes[1], out int den))
        {
            throw new ArgumentException("A string fornecida não está no formato 'numerador/denominador'.", nameof(fracaoString));
        }
        
        ArgumentOutOfRangeException.ThrowIfZero(den, nameof(den));
        
        Numerador = num;
        Denominador = den;
        Simplificar();
    }

 
    public Fracao(double valorDecimal)
    {
     
        string s = valorDecimal.ToString(CultureInfo.InvariantCulture);
        
        if (!s.Contains('.'))
        {
           
            Numerador = (int)valorDecimal;
            Denominador = 1;
        }
        else
        {
            
            int digitosDecimais = s.Length - s.IndexOf('.') - 1;
            Denominador = (int)Math.Pow(10, digitosDecimais);
            Numerador = (int)(valorDecimal * Denominador);
        }
        Simplificar();
    }

    #endregion

    #region Métodos de Instância e Consultas

   
    public Fracao Somar(int outroValor)
    {
        
        return this + new Fracao(outroValor);
    }
    
    
    public bool IsPropria => Math.Abs(Numerador) < Denominador;
    public bool IsImpropria => Math.Abs(Numerador) >= Denominador;
    public bool IsAparente => IsImpropria && Numerador % Denominador == 0;
    public bool IsUnitaria => Numerador == 1;

    #endregion

    #region Métodos Privados Auxiliares

    
    private static int MDC(int a, int b)
    {
        a = Math.Abs(a);
        b = Math.Abs(b);
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

   
    private void Simplificar()
    {
        
        if (Denominador < 0)
        {
            Numerador = -Numerador;
            Denominador = -Denominador;
        }

        if (Numerador == 0)
        {
            Denominador = 1;
            return;
        }

        int mdc = MDC(Numerador, Denominador);
        Numerador /= mdc;
        Denominador /= mdc;
    }

    #endregion

    #region Sobrecarga de Operadores

   
    public static Fracao operator +(Fracao a, Fracao b)
    {
        int novoNumerador = a.Numerador * b.Denominador + b.Numerador * a.Denominador;
        int novoDenominador = a.Denominador * b.Denominador;
        return new Fracao(novoNumerador, novoDenominador);
    }
    
   
    public static Fracao operator +(Fracao a, int b) => a + new Fracao(b);
    public static Fracao operator +(Fracao a, double b) => a + new Fracao(b);
    public static Fracao operator +(Fracao a, string b) => a + new Fracao(b);

    
    public static bool operator ==(Fracao a, Fracao b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;
        return a.Equals(b);
    }

    public static bool operator !=(Fracao a, Fracao b) => !(a == b);

    
    public static bool operator >(Fracao a, Fracao b) => (double)a.Numerador / a.Denominador > (double)b.Numerador / b.Denominador;
    public static bool operator <(Fracao a, Fracao b) => (double)a.Numerador / a.Denominador < (double)b.Numerador / b.Denominador;
    public static bool operator >=(Fracao a, Fracao b) => (double)a.Numerador / a.Denominador >= (double)b.Numerador / b.Denominador;
    public static bool operator <=(Fracao a, Fracao b) => (double)a.Numerador / a.Denominador <= (double)b.Numerador / b.Denominador;
    
    #endregion
    
    #region Sobrescrita de Métodos Padrão (ToString, Equals, GetHashCode)

        public override string ToString() => $"{Numerador}/{Denominador}";

   
    public override bool Equals(object obj) => obj is Fracao outra && Equals(outra);

   
    public bool Equals(Fracao other)
    {
        if (other is null) return false;
        
        return Numerador == other.Numerador && Denominador == other.Denominador;
    }

    public override int GetHashCode() => HashCode.Combine(Numerador, Denominador);
    
    #endregion
}