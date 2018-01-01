﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OcToDo.Data.DataBase
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="OcToDo")]
	public partial class OcToDoDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertPeople(People instance);
    partial void UpdatePeople(People instance);
    partial void DeletePeople(People instance);
    partial void InsertTeam(Team instance);
    partial void UpdateTeam(Team instance);
    partial void DeleteTeam(Team instance);
        #endregion

        public OcToDoDataContext() :
                base(global::OcToDo.Properties.Settings.Default.OcToDoConnectionString, mappingSource)
        {
            OnCreated();
        }

        public OcToDoDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public OcToDoDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public OcToDoDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public OcToDoDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<People> People
		{
			get
			{
				return this.GetTable<People>();
			}
		}
		
		public System.Data.Linq.Table<Team_content> Team_content
		{
			get
			{
				return this.GetTable<Team_content>();
			}
		}
		
		public System.Data.Linq.Table<Team> Team
		{
			get
			{
				return this.GetTable<Team>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.People")]
	public partial class People : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _People_ID;
		
		private string _FName;
		
		private string _MName;
		
		private string _LName;
		
		private string _Adress;
		
		private string _Phone;
		
		private System.Nullable<System.DateTime> _BirthDate;
		
		private string _UserName;
		
		private System.Nullable<int> _Telegram_ID;
		
		private EntitySet<Team> _Team;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnPeople_IDChanging(int value);
    partial void OnPeople_IDChanged();
    partial void OnFNameChanging(string value);
    partial void OnFNameChanged();
    partial void OnMNameChanging(string value);
    partial void OnMNameChanged();
    partial void OnLNameChanging(string value);
    partial void OnLNameChanged();
    partial void OnAdressChanging(string value);
    partial void OnAdressChanged();
    partial void OnPhoneChanging(string value);
    partial void OnPhoneChanged();
    partial void OnBirthDateChanging(System.Nullable<System.DateTime> value);
    partial void OnBirthDateChanged();
    partial void OnUserNameChanging(string value);
    partial void OnUserNameChanged();
    partial void OnTelegram_IDChanging(System.Nullable<int> value);
    partial void OnTelegram_IDChanged();
    #endregion
		
		public People()
		{
			this._Team = new EntitySet<Team>(new Action<Team>(this.attach_Team), new Action<Team>(this.detach_Team));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_People_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int People_ID
		{
			get
			{
				return this._People_ID;
			}
			set
			{
				if ((this._People_ID != value))
				{
					this.OnPeople_IDChanging(value);
					this.SendPropertyChanging();
					this._People_ID = value;
					this.SendPropertyChanged("People_ID");
					this.OnPeople_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FName", DbType="NVarChar(18)")]
		public string FName
		{
			get
			{
				return this._FName;
			}
			set
			{
				if ((this._FName != value))
				{
					this.OnFNameChanging(value);
					this.SendPropertyChanging();
					this._FName = value;
					this.SendPropertyChanged("FName");
					this.OnFNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MName", DbType="NVarChar(18)")]
		public string MName
		{
			get
			{
				return this._MName;
			}
			set
			{
				if ((this._MName != value))
				{
					this.OnMNameChanging(value);
					this.SendPropertyChanging();
					this._MName = value;
					this.SendPropertyChanged("MName");
					this.OnMNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LName", DbType="NVarChar(18)")]
		public string LName
		{
			get
			{
				return this._LName;
			}
			set
			{
				if ((this._LName != value))
				{
					this.OnLNameChanging(value);
					this.SendPropertyChanging();
					this._LName = value;
					this.SendPropertyChanged("LName");
					this.OnLNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Adress", DbType="NVarChar(40)")]
		public string Adress
		{
			get
			{
				return this._Adress;
			}
			set
			{
				if ((this._Adress != value))
				{
					this.OnAdressChanging(value);
					this.SendPropertyChanging();
					this._Adress = value;
					this.SendPropertyChanged("Adress");
					this.OnAdressChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Phone", DbType="Char(12)")]
		public string Phone
		{
			get
			{
				return this._Phone;
			}
			set
			{
				if ((this._Phone != value))
				{
					this.OnPhoneChanging(value);
					this.SendPropertyChanging();
					this._Phone = value;
					this.SendPropertyChanged("Phone");
					this.OnPhoneChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BirthDate", DbType="Date")]
		public System.Nullable<System.DateTime> BirthDate
		{
			get
			{
				return this._BirthDate;
			}
			set
			{
				if ((this._BirthDate != value))
				{
					this.OnBirthDateChanging(value);
					this.SendPropertyChanging();
					this._BirthDate = value;
					this.SendPropertyChanged("BirthDate");
					this.OnBirthDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserName", DbType="NVarChar(64)")]
		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				if ((this._UserName != value))
				{
					this.OnUserNameChanging(value);
					this.SendPropertyChanging();
					this._UserName = value;
					this.SendPropertyChanged("UserName");
					this.OnUserNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Telegram_ID", DbType="Int")]
		public System.Nullable<int> Telegram_ID
		{
			get
			{
				return this._Telegram_ID;
			}
			set
			{
				if ((this._Telegram_ID != value))
				{
					this.OnTelegram_IDChanging(value);
					this.SendPropertyChanging();
					this._Telegram_ID = value;
					this.SendPropertyChanged("Telegram_ID");
					this.OnTelegram_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="People_Team", Storage="_Team", ThisKey="People_ID", OtherKey="TeamLeader_ID")]
		public EntitySet<Team> Team
		{
			get
			{
				return this._Team;
			}
			set
			{
				this._Team.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Team(Team entity)
		{
			this.SendPropertyChanging();
			entity.People = this;
		}
		
		private void detach_Team(Team entity)
		{
			this.SendPropertyChanging();
			entity.People = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Team_content")]
	public partial class Team_content
	{
		
		private int _Team_ID;
		
		private int _People_ID;
		
		public Team_content()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Team_ID", DbType="Int NOT NULL")]
		public int Team_ID
		{
			get
			{
				return this._Team_ID;
			}
			set
			{
				if ((this._Team_ID != value))
				{
					this._Team_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_People_ID", DbType="Int NOT NULL")]
		public int People_ID
		{
			get
			{
				return this._People_ID;
			}
			set
			{
				if ((this._People_ID != value))
				{
					this._People_ID = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Team")]
	public partial class Team : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Team_ID;
		
		private int _TeamLeader_ID;
		
		private EntityRef<People> _People;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnTeam_IDChanging(int value);
    partial void OnTeam_IDChanged();
    partial void OnTeamLeader_IDChanging(int value);
    partial void OnTeamLeader_IDChanged();
    #endregion
		
		public Team()
		{
			this._People = default(EntityRef<People>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Team_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Team_ID
		{
			get
			{
				return this._Team_ID;
			}
			set
			{
				if ((this._Team_ID != value))
				{
					this.OnTeam_IDChanging(value);
					this.SendPropertyChanging();
					this._Team_ID = value;
					this.SendPropertyChanged("Team_ID");
					this.OnTeam_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TeamLeader_ID", DbType="Int NOT NULL")]
		public int TeamLeader_ID
		{
			get
			{
				return this._TeamLeader_ID;
			}
			set
			{
				if ((this._TeamLeader_ID != value))
				{
					if (this._People.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnTeamLeader_IDChanging(value);
					this.SendPropertyChanging();
					this._TeamLeader_ID = value;
					this.SendPropertyChanged("TeamLeader_ID");
					this.OnTeamLeader_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="People_Team", Storage="_People", ThisKey="TeamLeader_ID", OtherKey="People_ID", IsForeignKey=true)]
		public People People
		{
			get
			{
				return this._People.Entity;
			}
			set
			{
				People previousValue = this._People.Entity;
				if (((previousValue != value) 
							|| (this._People.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._People.Entity = null;
						previousValue.Team.Remove(this);
					}
					this._People.Entity = value;
					if ((value != null))
					{
						value.Team.Add(this);
						this._TeamLeader_ID = value.People_ID;
					}
					else
					{
						this._TeamLeader_ID = default(int);
					}
					this.SendPropertyChanged("People");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
