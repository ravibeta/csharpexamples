﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("NerdRideModel", "FK_RSVP_Rides", "Rides", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(NerdRide.Models.Ride), "RSVP", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(NerdRide.Models.RSVP), true)]

#endregion

namespace NerdRide.Models
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class NerdRideEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new NerdRideEntities object using the connection string found in the 'NerdRideEntities' section of the application configuration file.
        /// </summary>
        public NerdRideEntities() : base("name=NerdRideEntities", "NerdRideEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new NerdRideEntities object.
        /// </summary>
        public NerdRideEntities(string connectionString) : base(connectionString, "NerdRideEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new NerdRideEntities object.
        /// </summary>
        public NerdRideEntities(EntityConnection connection) : base(connection, "NerdRideEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Ride> Rides
        {
            get
            {
                if ((_Rides == null))
                {
                    _Rides = base.CreateObjectSet<Ride>("Rides");
                }
                return _Rides;
            }
        }
        private ObjectSet<Ride> _Rides;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<RSVP> RSVPs
        {
            get
            {
                if ((_RSVPs == null))
                {
                    _RSVPs = base.CreateObjectSet<RSVP>("RSVPs");
                }
                return _RSVPs;
            }
        }
        private ObjectSet<RSVP> _RSVPs;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Rides EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToRides(Ride ride)
        {
            base.AddObject("Rides", ride);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the RSVPs EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToRSVPs(RSVP rSVP)
        {
            base.AddObject("RSVPs", rSVP);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="NerdRideModel", Name="Ride")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Ride : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Ride object.
        /// </summary>
        /// <param name="rideID">Initial value of the RideID property.</param>
        /// <param name="title">Initial value of the Title property.</param>
        /// <param name="eventDate">Initial value of the EventDate property.</param>
        /// <param name="description">Initial value of the Description property.</param>
        /// <param name="hostedBy">Initial value of the HostedBy property.</param>
        /// <param name="contactPhone">Initial value of the ContactPhone property.</param>
        /// <param name="address">Initial value of the Address property.</param>
        /// <param name="country">Initial value of the Country property.</param>
        /// <param name="latitude">Initial value of the Latitude property.</param>
        /// <param name="longitude">Initial value of the Longitude property.</param>
        public static Ride CreateRide(global::System.Int32 rideID, global::System.String title, global::System.DateTime eventDate, global::System.String description, global::System.String hostedBy, global::System.String contactPhone, global::System.String address, global::System.String country, global::System.Double latitude, global::System.Double longitude)
        {
            Ride ride = new Ride();
            ride.RideID = rideID;
            ride.Title = title;
            ride.EventDate = eventDate;
            ride.Description = description;
            ride.HostedBy = hostedBy;
            ride.ContactPhone = contactPhone;
            ride.Address = address;
            ride.Country = country;
            ride.Latitude = latitude;
            ride.Longitude = longitude;
            return ride;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 RideID
        {
            get
            {
                return _RideID;
            }
            set
            {
                if (_RideID != value)
                {
                    OnRideIDChanging(value);
                    ReportPropertyChanging("RideID");
                    _RideID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("RideID");
                    OnRideIDChanged();
                }
            }
        }
        private global::System.Int32 _RideID;
        partial void OnRideIDChanging(global::System.Int32 value);
        partial void OnRideIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Title
        {
            get
            {
                return _Title;
            }
            set
            {
                OnTitleChanging(value);
                ReportPropertyChanging("Title");
                _Title = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Title");
                OnTitleChanged();
            }
        }
        private global::System.String _Title;
        partial void OnTitleChanging(global::System.String value);
        partial void OnTitleChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime EventDate
        {
            get
            {
                return _EventDate;
            }
            set
            {
                OnEventDateChanging(value);
                ReportPropertyChanging("EventDate");
                _EventDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("EventDate");
                OnEventDateChanged();
            }
        }
        private global::System.DateTime _EventDate;
        partial void OnEventDateChanging(global::System.DateTime value);
        partial void OnEventDateChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                OnDescriptionChanging(value);
                ReportPropertyChanging("Description");
                _Description = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Description");
                OnDescriptionChanged();
            }
        }
        private global::System.String _Description;
        partial void OnDescriptionChanging(global::System.String value);
        partial void OnDescriptionChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String HostedBy
        {
            get
            {
                return _HostedBy;
            }
            set
            {
                OnHostedByChanging(value);
                ReportPropertyChanging("HostedBy");
                _HostedBy = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("HostedBy");
                OnHostedByChanged();
            }
        }
        private global::System.String _HostedBy;
        partial void OnHostedByChanging(global::System.String value);
        partial void OnHostedByChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String ContactPhone
        {
            get
            {
                return _ContactPhone;
            }
            set
            {
                OnContactPhoneChanging(value);
                ReportPropertyChanging("ContactPhone");
                _ContactPhone = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("ContactPhone");
                OnContactPhoneChanged();
            }
        }
        private global::System.String _ContactPhone;
        partial void OnContactPhoneChanging(global::System.String value);
        partial void OnContactPhoneChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Address
        {
            get
            {
                return _Address;
            }
            set
            {
                OnAddressChanging(value);
                ReportPropertyChanging("Address");
                _Address = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Address");
                OnAddressChanged();
            }
        }
        private global::System.String _Address;
        partial void OnAddressChanging(global::System.String value);
        partial void OnAddressChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Country
        {
            get
            {
                return _Country;
            }
            set
            {
                OnCountryChanging(value);
                ReportPropertyChanging("Country");
                _Country = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Country");
                OnCountryChanged();
            }
        }
        private global::System.String _Country;
        partial void OnCountryChanging(global::System.String value);
        partial void OnCountryChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Double Latitude
        {
            get
            {
                return _Latitude;
            }
            set
            {
                OnLatitudeChanging(value);
                ReportPropertyChanging("Latitude");
                _Latitude = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Latitude");
                OnLatitudeChanged();
            }
        }
        private global::System.Double _Latitude;
        partial void OnLatitudeChanging(global::System.Double value);
        partial void OnLatitudeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Double Longitude
        {
            get
            {
                return _Longitude;
            }
            set
            {
                OnLongitudeChanging(value);
                ReportPropertyChanging("Longitude");
                _Longitude = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Longitude");
                OnLongitudeChanged();
            }
        }
        private global::System.Double _Longitude;
        partial void OnLongitudeChanging(global::System.Double value);
        partial void OnLongitudeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String HostedById
        {
            get
            {
                return _HostedById;
            }
            set
            {
                OnHostedByIdChanging(value);
                ReportPropertyChanging("HostedById");
                _HostedById = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("HostedById");
                OnHostedByIdChanged();
            }
        }
        private global::System.String _HostedById;
        partial void OnHostedByIdChanging(global::System.String value);
        partial void OnHostedByIdChanged();

        #endregion

    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("NerdRideModel", "FK_RSVP_Rides", "RSVP")]
        public EntityCollection<RSVP> RSVPs
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<RSVP>("NerdRideModel.FK_RSVP_Rides", "RSVP");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<RSVP>("NerdRideModel.FK_RSVP_Rides", "RSVP", value);
                }
            }
        }

        #endregion

    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="NerdRideModel", Name="RSVP")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class RSVP : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new RSVP object.
        /// </summary>
        /// <param name="rsvpID">Initial value of the RsvpID property.</param>
        /// <param name="rideID">Initial value of the RideID property.</param>
        /// <param name="attendeeName">Initial value of the AttendeeName property.</param>
        public static RSVP CreateRSVP(global::System.Int32 rsvpID, global::System.Int32 rideID, global::System.String attendeeName)
        {
            RSVP rSVP = new RSVP();
            rSVP.RsvpID = rsvpID;
            rSVP.RideID = rideID;
            rSVP.AttendeeName = attendeeName;
            return rSVP;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 RsvpID
        {
            get
            {
                return _RsvpID;
            }
            set
            {
                if (_RsvpID != value)
                {
                    OnRsvpIDChanging(value);
                    ReportPropertyChanging("RsvpID");
                    _RsvpID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("RsvpID");
                    OnRsvpIDChanged();
                }
            }
        }
        private global::System.Int32 _RsvpID;
        partial void OnRsvpIDChanging(global::System.Int32 value);
        partial void OnRsvpIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 RideID
        {
            get
            {
                return _RideID;
            }
            set
            {
                OnRideIDChanging(value);
                ReportPropertyChanging("RideID");
                _RideID = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("RideID");
                OnRideIDChanged();
            }
        }
        private global::System.Int32 _RideID;
        partial void OnRideIDChanging(global::System.Int32 value);
        partial void OnRideIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String AttendeeName
        {
            get
            {
                return _AttendeeName;
            }
            set
            {
                OnAttendeeNameChanging(value);
                ReportPropertyChanging("AttendeeName");
                _AttendeeName = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("AttendeeName");
                OnAttendeeNameChanged();
            }
        }
        private global::System.String _AttendeeName;
        partial void OnAttendeeNameChanging(global::System.String value);
        partial void OnAttendeeNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String AttendeeNameId
        {
            get
            {
                return _AttendeeNameId;
            }
            set
            {
                OnAttendeeNameIdChanging(value);
                ReportPropertyChanging("AttendeeNameId");
                _AttendeeNameId = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("AttendeeNameId");
                OnAttendeeNameIdChanged();
            }
        }
        private global::System.String _AttendeeNameId;
        partial void OnAttendeeNameIdChanging(global::System.String value);
        partial void OnAttendeeNameIdChanged();

        #endregion

    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("NerdRideModel", "FK_RSVP_Rides", "Rides")]
        public Ride Ride
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Ride>("NerdRideModel.FK_RSVP_Rides", "Rides").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Ride>("NerdRideModel.FK_RSVP_Rides", "Rides").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Ride> RideReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Ride>("NerdRideModel.FK_RSVP_Rides", "Rides");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Ride>("NerdRideModel.FK_RSVP_Rides", "Rides", value);
                }
            }
        }

        #endregion

    }

    #endregion

    
}
