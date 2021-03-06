//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BillingWeb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        public tblProduct()
        {
            this.tblDeliveryNoteItems = new HashSet<tblDeliveryNoteItem>();
            this.tblInventoryItems = new HashSet<tblInventoryItem>();
            this.tblInvoiceItems = new HashSet<tblInvoiceItem>();
            this.tblQuotationItems = new HashSet<tblQuotationItem>();
        }

        public int ProductID { get; set; }

        
        [Required(ErrorMessage = "Select Product Category")]
        public Nullable<int> ProductCategoryID { get; set; }

        
        [Required(ErrorMessage = "Select Product SubCategory")]
        public Nullable<int> ProductSubCategoryID { get; set; }

        [Required(ErrorMessage = "Please enter product name.")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Product Description")]

        public string ProductDescription { get; set; }
        [Display(Name = "Make")]
        public string Make { get; set; }
        [Display(Name = "Tax")]
        public Nullable<int> TaxID { get; set; }
        [Display(Name = "Size")]
        [Required(ErrorMessage = "Select Size")]

        public Nullable<int> SizeID { get; set; }
        [Display(Name = "Rate Per Unit")]
        public Nullable<decimal> RatePerUnit { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        [Display(Name = "Unit")]
        [Required(ErrorMessage = "Select Unit")]
        public Nullable<int> UnitID { get; set; }

        [Display(Name = "SGST")]

        [Required(ErrorMessage = "SGST is required.")]

        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Valid Decimal number with maximum 2 decimal places.")]
        public Nullable<decimal> SGST { get; set; }
        [Display(Name = "CGST")]

        [Required(ErrorMessage = "CGST is required.")]

        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Valid Decimal number with maximum 2 decimal places.")]
        public Nullable<decimal> CGST { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblDeliveryNoteItem> tblDeliveryNoteItems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblInventoryItem> tblInventoryItems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblInvoiceItem> tblInvoiceItems { get; set; }
        public virtual tblProductCategory tblProductCategory { get; set; }
        public virtual tblProductSubCategory tblProductSubCategory { get; set; }
        public virtual tblSize tblSize { get; set; }
        public virtual tblTax tblTax { get; set; }
        public virtual tblUnit tblUnit { get; set; }
        public virtual tblUser tblUser { get; set; }
        public virtual tblUser tblUser1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblQuotationItem> tblQuotationItems { get; set; }
    }
}
