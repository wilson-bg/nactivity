﻿using System;

/* Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *      http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
namespace Sys.Workflow.Engine.Impl.Variable
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Bson;
    using Newtonsoft.Json.Linq;
    using Sys.Workflow.Engine.Impl.Contexts;
    using Sys.Workflow.Engine.Impl.Persistence.Entity;
    using Sys.Workflow.Engine.Impl.Util;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;

    /// 
    /// 
    public class SerializableType : ByteArrayType
    {

        public const string TYPE_NAME = "serializable";

        protected internal bool trackDeserializedObjects;

        public override string TypeName
        {
            get
            {
                return TYPE_NAME;
            }
        }

        public SerializableType()
        {

        }

        public SerializableType(bool trackDeserializedObjects)
        {
            this.trackDeserializedObjects = trackDeserializedObjects;
        }

        public override object GetValue(IValueFields valueFields)
        {
            object cachedObject = valueFields.CachedValue;
            if (cachedObject is not null)
            {
                return cachedObject;
            }

            byte[] bytes = (byte[])base.GetValue(valueFields);
            if (bytes is not null)
            {
                object deserializedObject = Deserialize(bytes, valueFields);
                valueFields.CachedValue = deserializedObject;

                if (trackDeserializedObjects && valueFields is IVariableInstanceEntity)
                {
                    Context.CommandContext.AddCloseListener(new VerifyDeserializedObjectCommandContextCloseListener(new DeserializedObject(this, valueFields.CachedValue, bytes, (IVariableInstanceEntity)valueFields)));
                }

                return deserializedObject;
            }
            return null; // byte array is null
        }

        public override void SetValue(object value, IValueFields valueFields)
        {
            byte[] bytes = Serialize(value, valueFields);
            valueFields.CachedValue = value;

            base.SetValue(bytes, valueFields);

            if (trackDeserializedObjects && valueFields is IVariableInstanceEntity)
            {
                Context.CommandContext.AddCloseListener(new VerifyDeserializedObjectCommandContextCloseListener(new DeserializedObject(this, valueFields.CachedValue, bytes, (IVariableInstanceEntity)valueFields)));
            }

        }

        public virtual byte[] Serialize(object value, IValueFields valueFields)
        {
            if (value is null)
            {
                return null;
            }

            try
            {
                return Serializer.Serialize(value);
            }
            catch (Exception e)
            {
                throw new ActivitiException("Couldn't serialize value '" + value + "' in variable '" + valueFields.Name + "'", e);
            }
        }

        public virtual object Deserialize(byte[] bytes, IValueFields valueFields)
        {
            try
            {
                return Serializer.Deserialize(bytes);
            }
            catch (Exception e)
            {
                throw new ActivitiException("Couldn't deserialize object in variable '" + valueFields.Name + "'", e);
            }
        }

        public override bool IsAbleToStore(object value)
        {
            // TODO don't we need null support here?
            //return value is Serializable;
            return true;
        }
    }

}