﻿using System.ComponentModel;
using DevExpress.ExpressApp.Utils;
using DevExpress.Xpo;
using eXpand.ExpressApp.Attributes;
using eXpand.Persistent.Base.General;
using eXpand.Persistent.Base.PersistentMetaData;
using eXpand.Persistent.Base.PersistentMetaData.PersistentAttributeInfos;

namespace eXpand.Persistent.BaseImpl.PersistentMetaData.PersistentAttributeInfos {
    [HideFromNewMenu]
    [InterfaceRegistrator(typeof(IPersistentAssemblyDataStoreAttributeInfo))]
    public class PersistentAssemblyDataStoreAttributeInfo : PersistentAssemblyAttributeInfo, IPersistentAssemblyDataStoreAttributeInfo {
        public PersistentAssemblyDataStoreAttributeInfo(Session session) : base(session) {
        }
        
        
        private DataStoreLogonObject _dataStoreLogon;
        private string _connectionString;

        [Browsable(false)]
        [Size(SizeAttribute.Unlimited)]
        public string ConnectionString {
            get { return _connectionString; }
            set { SetPropertyValue("ConnectionString", ref _connectionString, value); }
        }
        public override AttributeInfo Create() {
            var constructorInfo = typeof(DataStoreAttribute).GetConstructor(new[]{typeof(string),typeof(string)});
            string initializedArgumentValues = null;
            if (PersistentClassInfo.PersistentAssemblyInfo != null)
                initializedArgumentValues = PersistentClassInfo.PersistentAssemblyInfo.Name+"."+PersistentClassInfo.Name;
            Guard.ArgumentNotNull(ConnectionString, "ConnectionString");
            return new AttributeInfo(constructorInfo, _connectionString,initializedArgumentValues);
        }
        
        [NonPersistent]
        public DataStoreLogonObject DataStoreLogon {
            get { return _dataStoreLogon; }
            set {
                SetPropertyValue("DataStoreLogon", ref _dataStoreLogon, value);
                _connectionString = value.GetConnectionString();
            }
        }
        private PersistentClassInfo _persistentClassInfo;
        public PersistentClassInfo PersistentClassInfo
        {
            get { return _persistentClassInfo; }
            set { SetPropertyValue("PersistentClassInfo", ref _persistentClassInfo, value); }
        }
        IPersistentClassInfo IPersistentAssemblyDataStoreAttributeInfo.PersistentClassInfo {
            get { return PersistentClassInfo; }
            set { PersistentClassInfo=value as PersistentClassInfo; }
        }

        IDataStoreLogonObject IPersistentAssemblyDataStoreAttributeInfo.DataStoreLogon {
            get { return DataStoreLogon; }
            set { DataStoreLogon=value as DataStoreLogonObject; }
        }
    }
}