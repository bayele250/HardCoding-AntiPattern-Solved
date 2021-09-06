using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorPatternExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creating Membership for SilverMembership type
            IMembership silverMembership = new SilverMembership();

            // Adding annual free to the silver membership
            IMembership annualFeeDecorator = new AnnualFeeDecorator(silverMembership);

            // Adding parking fee to the silver membership
            IMembership parkingFeeDecorator = new ParkingFeeDecorator(annualFeeDecorator);

            // Adding free reservation cancelation benefit to the silver membership
            IMembership freeCancelationBenefitDecorator = new CacelationBenefitDecorator(parkingFeeDecorator);

            Console.WriteLine("Silver Membership Benefits");
            Console.WriteLine(freeCancelationBenefitDecorator.GetBenefits());

            Console.WriteLine("Silver Membership Fee: ");
            Console.WriteLine(freeCancelationBenefitDecorator.GetFee());

            Console.WriteLine();
            //Creating Membership for GoldMembership type
            IMembership goldMembership = new GoldMembership();

            // Adding annual fee to the gold membership
            IMembership goldAnnualFeeDecorator = new AnnualFeeDecorator(goldMembership);

            // Adding Initial fee to the gold membership
            IMembership goldInitialFeeDecorator = new InitialFeeDecorator(goldAnnualFeeDecorator);

            // Adding free reservation cancelation fee to the gold membership
            IMembership goldFreeCancelationBenefirDecorator = new CacelationBenefitDecorator(goldInitialFeeDecorator);

            // Adding free shuttle benefit to the gold membership
            IMembership goldFreeshuttleBenefitDecorator = new ShuttleBenefitDecorator(goldFreeCancelationBenefirDecorator);

            // Adding free meal benefit to the gold membership
            IMembership goldMealBenefitDecorator = new MealBenefitDecorator(goldFreeshuttleBenefitDecorator);


            Console.WriteLine("Gold Membership Benefits");
            Console.WriteLine(goldMealBenefitDecorator.GetBenefits());

            Console.WriteLine("Gold Membership Fee: ");
            Console.WriteLine(goldMealBenefitDecorator.GetFee());


            Console.WriteLine();
            //Creating Membership for Platinium type
            IMembership platiniumMembership = new PlatinumMembership();

            // Adding annual fee to the platinium membership
            IMembership platiniumAnnualFeeDecorator = new AnnualFeeDecorator(platiniumMembership);

            // Adding Initial fee to the platinium membership
            IMembership platiniumInitialFeeDecorator = new InitialFeeDecorator(platiniumAnnualFeeDecorator);

            // Adding free reservation cancelation fee to the platinium membership
            IMembership platiniumFreeCancelationBenefirDecorator = new CacelationBenefitDecorator(platiniumInitialFeeDecorator);

            // Adding free shuttle benefit to the platinium membership
            IMembership platiniumFreeshuttleBenefitDecorator = new ShuttleBenefitDecorator(platiniumFreeCancelationBenefirDecorator);

            // Adding free meal benefit to the platinium membership
            IMembership platiniumMealBenefitDecorator = new MealBenefitDecorator(platiniumFreeshuttleBenefitDecorator);

            // Adding free tour guide benefit to the platinium membership
            IMembership platiniumTourGuideBenefitDecorator = new FreeTourGuideBenefitDecorator(platiniumMealBenefitDecorator);


            Console.WriteLine("Platinium Membership Benefits");
            Console.WriteLine(platiniumTourGuideBenefitDecorator.GetBenefits());

            Console.WriteLine("Platinium Membership Fee: ");
            Console.WriteLine(platiniumTourGuideBenefitDecorator.GetFee());

            Console.ReadKey();
        }
    }

    public interface IMembership
    {
        double GetFee();
        string GetBenefits();
    }

    public class GoldMembership : IMembership
    {
        public double fee { get; set; }
        public string GetBenefits()
        {
            return "\n"  + ConfigurationProperties.goldMembershipBenefit + "\n";
        }

        public double GetFee()
        {
            this.fee = ConfigurationProperties.goldMembershipFee;
            return this.fee;
        }
    }
    public class SilverMembership : IMembership
    {
        public double fee { get; set; }
        public string GetBenefits()
        {
            return "\n" + ConfigurationProperties.silverMembershipBenefit + "\n";
        }

        public double GetFee()
        {
            this.fee = ConfigurationProperties.silverMembershipFee;
            return this.fee;
        }
    }
    public class PlatinumMembership : IMembership
    {
        public double fee { get; set; }
        public string GetBenefits()
        {
            return "\n"  + ConfigurationProperties.platiniumMembershipBenefit + "\n";
        }

        public double GetFee()
        {
            this.fee = ConfigurationProperties.platiniumMembershipFee;
            return this.fee;
        }
    }

    public abstract class MembershipDecorator : IMembership
    {
        public IMembership _membership;
        public MembershipDecorator(IMembership membership)
        {
            this._membership = membership;
        }
        public virtual double GetFee()
        {
            return _membership.GetFee();
        }

        public virtual string GetBenefits()
        {
            return _membership.GetBenefits();
        }

    }

    public class AnnualFeeDecorator : MembershipDecorator
    {
        public AnnualFeeDecorator(IMembership membership) : base(membership) { }

        public override double GetFee()
        {
            return base.GetFee() + ConfigurationProperties.annualFeeDecoratorValue;
        }
    }

    public class InitialFeeDecorator : MembershipDecorator
    {
        public InitialFeeDecorator(IMembership membership) : base(membership) { }

        public override double GetFee()
        {
            return base.GetFee() + ConfigurationProperties.initialFeeDecoratorValue;
        }
    }

    public class ParkingFeeDecorator : MembershipDecorator
    {
        public ParkingFeeDecorator(IMembership membership) : base(membership) { }

        public override double GetFee()
        {
            return base.GetFee() + ConfigurationProperties.parkingFeeDecoratorValue;
        }
    }

    public class CacelationBenefitDecorator : MembershipDecorator
    {
        public CacelationBenefitDecorator(IMembership membership) : base(membership) { }

        public override string GetBenefits()
        {
            return base.GetBenefits() + "\n" + ConfigurationProperties.cacelationBenefitDecorator + "\n";
        }
    }

    public class MealBenefitDecorator : MembershipDecorator
    {
        public MealBenefitDecorator(IMembership membership) : base(membership) { }

        public override string GetBenefits()
        {
            return base.GetBenefits() + "\n"  + ConfigurationProperties.mealBenefitDecorator + "\n";
        }
    }

    public class ShuttleBenefitDecorator : MembershipDecorator
    {
        public ShuttleBenefitDecorator(IMembership membership) : base(membership) { }

        public override string GetBenefits()
        {
            return base.GetBenefits() + "\n" + ConfigurationProperties.shuttleBenefitDecorator + "\n";
        }
    }

    public class FreeTourGuideBenefitDecorator : MembershipDecorator
    {
        public FreeTourGuideBenefitDecorator(IMembership membership) : base(membership) { }

        public override string GetBenefits()
        {
            return base.GetBenefits() + "\n" + ConfigurationProperties.freeTourGuideBenefitDecorator + "\n";
        }
    }


    public static class ConfigurationProperties
    {

        public static double silverMembershipFee = Double.Parse(ConfigurationManager.AppSettings["silverMembershipFee"].ToString());
        public static double goldMembershipFee = Double.Parse(ConfigurationManager.AppSettings["goldMembershipFee"].ToString());
        public static double platiniumMembershipFee  = Double.Parse(ConfigurationManager.AppSettings["platiniumMembershipFee"].ToString());

        public static double annualFeeDecoratorValue = Double.Parse(ConfigurationManager.AppSettings["annualFeeDecoratorValue"].ToString());
        public static double initialFeeDecoratorValue = Double.Parse(ConfigurationManager.AppSettings["initialFeeDecoratorValue"].ToString());
        public static double parkingFeeDecoratorValue = Double.Parse(ConfigurationManager.AppSettings["parkingFeeDecoratorValue"].ToString());


        public static string silverMembershipBenefit = ConfigurationManager.AppSettings["silverMembershipBenefit"].ToString();
        public static string goldMembershipBenefit = ConfigurationManager.AppSettings["goldMembershipBenefit"].ToString();
        public static string platiniumMembershipBenefit = ConfigurationManager.AppSettings["platiniumMembershipBenefit"].ToString();

        public static string cacelationBenefitDecorator = ConfigurationManager.AppSettings["cacelationBenefitDecorator"].ToString();
        public static string mealBenefitDecorator = ConfigurationManager.AppSettings["mealBenefitDecorator"].ToString();
        public static string shuttleBenefitDecorator = ConfigurationManager.AppSettings["shuttleBenefitDecorator"].ToString();
        public static string freeTourGuideBenefitDecorator = ConfigurationManager.AppSettings["freeTourGuideBenefitDecorator"].ToString();


    }


}

