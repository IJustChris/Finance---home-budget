using Finance.Core.Domain.Exceptions;
using Finance.Core.Domain.Extensions;

namespace Finance.Core.Domain
{
    public class Category
    {
        public int CategoryId { get; protected set; }
        public int ParentId { get; protected set; }
        public int UserId { get; protected set; }
        public string IconName { get; protected set; }
        public string ColorHex { get; protected set; }
        public string Name { get; protected set; }
        public int? BudgetId { get; protected set; }

        // CONSTRUCTORS
        protected Category()
        {

        }

        protected Category(int id, string name, int userId, int parentId, string iconName, string color)
        {
            if (id.isEmpty() || userId.isEmpty() || parentId.isEmpty() || iconName.isEmpty())
                throw new DomainException(DomainErrorCodes.EmptyId);

            SetName(name);
            SetColorHex(color);
            CategoryId = id;
            ParentId = parentId;
            UserId = userId;
            SetIconId(iconName);
        }

        protected Category(int id, string name, int userId, string iconName, string color)
        {
            if (id.isEmpty() || userId.isEmpty())
                throw new DomainException(DomainErrorCodes.EmptyId);

            SetName(name);
            SetColorHex(color);
            SetIconId(iconName);
            CategoryId = id;
            ParentId = id;
            UserId = userId;

        }


        public static Category Create(int id, string name, int userId, string iconName, string color)
            => new Category(id, name, userId, iconName, color);

        public Category CreateSubcategory(int id, string name, string iconName, string color)
        {
            if (CategoryId != ParentId)
            {
                throw new DomainException(DomainErrorCodes.SubcategoryBuisnessLogicError, "Cant create subcategory for subcategory");
            }

            var category = new Category(id, name, UserId, CategoryId, iconName, color);

            return category;
        }

        // PUBLIC SETTERS

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException(DomainErrorCodes.InvalidCategoryName, "Category name cant be empty!");
            }
            if (name.Length > 20)
            {
                throw new DomainException(DomainErrorCodes.InvalidCategoryName, "Category name cant be longer than 20 characters");
            }

            Name = name;
        }

        public void SetColorHex(string color)
        {
            color = color.ToUpper();

            if (!color.isHexColor())
                throw new DomainException(DomainErrorCodes.InvalidColor);

            ColorHex = color;
        }

        public void SetIconId(string iconName)
        {
            if (iconName.isEmpty())
                throw new DomainException(DomainErrorCodes.EmptyString);

            IconName = iconName;
        }

    }
}

