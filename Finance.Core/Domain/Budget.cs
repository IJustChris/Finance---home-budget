using Finance.Core.Domain.Exceptions;
using Finance.Core.Domain.Extensions;
using System.Collections.Generic;
using System.Text;

namespace Finance.Core.Domain
{
    public class Budget
    {
        // EVENTS
        public delegate decimal GetActualBudgetExpenditureEventHandler(IEnumerable<int> categoriesID);
        public event GetActualBudgetExpenditureEventHandler GetActualBudgetExpenditureEvent;


        // PROPERTIES

        private ISet<int> _categoriesId = new HashSet<int>();

        public int BudgetId { get; protected set; }
        public int UserId { get; protected set; }
        public string Name { get; protected set; }
        public string ColorHex { get; protected set; }
        public string IconName { get; protected set; }
        public decimal BudgetLimit { get; protected set; }
        public IEnumerable<int> CategoriesId => _categoriesId;
        public decimal ActualExpenditure { get; protected set; }


        //CONSTRUCTORS

        protected Budget()
        {

        }

        protected Budget(string name, int id, string colorHex, string iconName, decimal budgetValue, IEnumerable<int> categoriesID, int userId)
        {
            if (id.isEmpty() || userId.isEmpty())
                throw new DomainException(DomainErrorCodes.EmptyId);

            UserId = userId;
            BudgetId = id;
            SetName(name);
            SetColor(colorHex);
            SetIconName(iconName);
            SetBudgetLimit(budgetValue);

            foreach (var catId in categoriesID)
                AddCategoryID(catId);
        }

        public static Budget Create(string name, int id, string colorHex, string iconName, decimal budgetValue, IEnumerable<int> categoriesID, int userId)
           => new Budget(name, id, colorHex, iconName, budgetValue, categoriesID, userId);

        // SETTERS

        public void SetName(string name)
        {
            if (name.isEmpty())
                throw new DomainException(DomainErrorCodes.EmptyString);

            Name = name;
        }

        public void SetColor(string color)
        {
            color = color.ToUpper();

            if (color?[0] == '#' || color.Length == 6)
            {
                if (color.Length == 6 && color[0] != '#')
                {
                    color = new StringBuilder("#").Append(color).ToString();
                }

                foreach (var ch in color.Substring(1))
                {
                    if (!ch.isNumber() && !ch.isUpperCaseHexCharacter())
                        throw new DomainException(DomainErrorCodes.InvalidColor);
                }
            }
            else
                throw new DomainException(DomainErrorCodes.InvalidColor);

            ColorHex = color;
        }

        public void SetIconName(string iconName)
            => IconName = iconName;

        public void SetBudgetLimit(decimal budgetValue)
        {
            if (budgetValue < 0)
                throw new DomainException(DomainErrorCodes.ValueTooLow);
            else
                BudgetLimit = budgetValue;

        }

        public void AddCategoryID(int catId)
            => _categoriesId.Add(catId);

        public void RemoveCategoryID(int catId)
        {
            if (_categoriesId.Contains(catId))
                _categoriesId.Remove(catId);

        }

        // CONTROLS

        protected decimal GetExpanditure()
        {
            if (GetActualBudgetExpenditureEvent != null)
                return GetActualBudgetExpenditureEvent(CategoriesId);

            throw new DomainException(DomainErrorCodes.BudgetEventNotAssignedToAnyUser);
        }


    }
}
