﻿/* Licensed under the Apache License, Version 2.0 (the "License");
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
using Sys.Workflow.Bpmn.Converters;
using Sys.Workflow.Bpmn.Models;
using System.Xml.Linq;

namespace Sys.Workflow.Bpmn.Constants
{
	public interface IBpmnXMLConstants
	{
		// fake element for mail task
		// only used by valued data objects
	}

	public static class BpmnXMLConstants
	{
		public const string BPMN2_NAMESPACE = "http://www.omg.org/spec/BPMN/20100524/MODEL";
		public const string XSI_NAMESPACE = "http://www.w3.org/2001/XMLSchema-instance";
		public const string XSI_PREFIX = "xsi";
		public const string BPMN2_PREFIX = "bpmn2";
		public const string XMLNS_PREFIX = "xmlns";
		public const string XMLNS_NAMESPACE = "http://www.w3.org/2000/xmlns/";
		public const string SCHEMA_NAMESPACE = "http://www.w3.org/2001/XMLSchema";
		public const string XSD_PREFIX = "xsd";
		public const string TYPE_LANGUAGE_ATTRIBUTE = "typeLanguage";
		public const string XPATH_NAMESPACE = "http://www.w3.org/1999/XPath";
		public const string EXPRESSION_LANGUAGE_ATTRIBUTE = "expressionLanguage";
		public const string PROCESS_NAMESPACE = "http://www.activiti.org/test";
		public const string TARGET_NAMESPACE_ATTRIBUTE = "targetNamespace";
		public const string ACTIVITI_EXTENSIONS_NAMESPACE = "http://camunda.org/schema/1.0/bpmn";//"http://activiti.org/bpmn";
		public const string ACTIVITI_EXTENSIONS_PREFIX = "camunda";
		public const string BPMNDI_NAMESPACE = "http://www.omg.org/spec/BPMN/20100524/DI";
		public const string BPMNDI_PREFIX = "bpmndi";
		public const string OMGDC_NAMESPACE = "http://www.omg.org/spec/DD/20100524/DC";
		public const string OMGDC_PREFIX = "dc";
		public const string OMGDI_NAMESPACE = "http://www.omg.org/spec/DD/20100524/DI";
		public const string OMGDI_PREFIX = "di";
		public const string SAMPLE_DIAGRAM = "sample-diagram";
		public const string SCHEMA_LOCATION = "schemaLocation";
		public const string SCHEMA_LOCATION_MODEL_BPMN2 = "http://www.omg.org/spec/BPMN/20100524/MODEL BPMN20.xsd";

		public const string ELEMENT_DEFINITIONS = "definitions";
		public const string ELEMENT_DOCUMENTATION = "documentation";
		public const string ELEMENT_SIGNAL = "signal";
		public const string ELEMENT_MESSAGE = "message";
		public const string ELEMENT_ERROR = "error";
		public const string ELEMENT_COLLABORATION = "collaboration";
		public const string ELEMENT_PARTICIPANT = "participant";
		public const string ELEMENT_MESSAGE_FLOW = "messageFlow";
		public const string ELEMENT_LANESET = "laneSet";
		public const string ELEMENT_LANE = "lane";
		public const string ELEMENT_FLOWNODE_REF = "flowNodeRef";
		public const string ELEMENT_RESOURCE = "resource";
		public const string ELEMENT_PROCESS = "process";
		public const string ELEMENT_POTENTIAL_STARTER = "potentialStarter";
		public const string ELEMENT_SUBPROCESS = "subProcess";
		public const string ELEMENT_TRANSACTION = "transaction";
		public const string ELEMENT_ADHOC_SUBPROCESS = "adHocSubProcess";
		public const string ELEMENT_COMPLETION_CONDITION = "completionCondition";
		public const string ELEMENT_DATA_STATE = "dataState";
		public const string ELEMENT_EXTENSIONS = "extensionElements";
		public const string ELEMENT_EXECUTION_LISTENER = "executionListener";
		public const string ELEMENT_EVENT_LISTENER = "eventListener";
		public const string ELEMENT_TASK_LISTENER = "taskListener";
		public const string ELEMENT_IOSPECIFICATION = "ioSpecification";
		public const string ELEMENT_IMPORT = "import";
		public const string ELEMENT_INTERFACE = "interface";
		public const string ELEMENT_OPERATION = "operation";
		public const string ELEMENT_IN_MESSAGE = "inMessageRef";
		public const string ELEMENT_OUT_MESSAGE = "outMessageRef";
		public const string ELEMENT_ITEM_DEFINITION = "itemDefinition";
		public const string ELEMENT_DATA_STORE = "dataStore";
		public const string ELEMENT_DATA_STORE_REFERENCE = "dataStoreReference";
		public const string ELEMENT_DATA_INPUT = "dataInput";
		public const string ELEMENT_DATA_OUTPUT = "dataOutput";
		public const string ELEMENT_DATA_INPUT_REFS = "dataInputRefs";
		public const string ELEMENT_DATA_OUTPUT_REFS = "dataOutputRefs";
		public const string ELEMENT_INPUT_ASSOCIATION = "dataInputAssociation";
		public const string ELEMENT_OUTPUT_ASSOCIATION = "dataOutputAssociation";
		public const string ELEMENT_SOURCE_REF = "sourceRef";
		public const string ELEMENT_TARGET_REF = "targetRef";
		public const string ELEMENT_TRANSFORMATION = "transformation";
		public const string ELEMENT_ASSIGNMENT = "assignment";
		public const string ELEMENT_FROM = "from";
		public const string ELEMENT_TO = "to";
		public const string ELEMENT_TASK_MAIL = "mailTask";
		public const string ELEMENT_TASK = "task";
		public const string ELEMENT_TASK_BUSINESSRULE = "businessRuleTask";
		public const string ELEMENT_TASK_MANUAL = "manualTask";
		public const string ELEMENT_TASK_RECEIVE = "receiveTask";
		public const string ELEMENT_TASK_SCRIPT = "scriptTask";
		public const string ELEMENT_TASK_SEND = "sendTask";
		public const string ELEMENT_TASK_SERVICE = "serviceTask";
		public const string ELEMENT_TASK_USER = "userTask";
		public const string ELEMENT_CALL_ACTIVITY = "callActivity";
		public const string ELEMENT_MULTIINSTANCE = "multiInstanceLoopCharacteristics";
		public const string ELEMENT_MULTIINSTANCE_CARDINALITY = "loopCardinality";
		public const string ELEMENT_MULTIINSTANCE_DATAINPUT = "loopDataInputRef";
		public const string ELEMENT_MULTIINSTANCE_DATAITEM = "inputDataItem";
		public const string ELEMENT_MULTIINSTANCE_CONDITION = "completionCondition";
		public const string ELEMENT_USER_TASK_EXTENSION_ASSIGNE_TYPE = "assigneeType";
		public const string ELEMENT_CALL_ACTIVITY_IN_PARAMETERS = "in";
		public const string ELEMENT_CALL_ACTIVITY_OUT_PARAMETERS = "out";
		public const string ELEMENT_SEQUENCE_FLOW = "sequenceFlow";
		public const string ELEMENT_FLOW_CONDITION = "conditionExpression";
		public const string ELEMENT_TEXT_ANNOTATION = "textAnnotation";
		public const string ELEMENT_TEXT_ANNOTATION_TEXT = "text";
		public const string ELEMENT_ASSOCIATION = "association";
		public const string ELEMENT_GATEWAY_EXCLUSIVE = "exclusiveGateway";
		public const string ELEMENT_GATEWAY_EVENT = "eventBasedGateway";
		public const string ELEMENT_GATEWAY_INCLUSIVE = "inclusiveGateway";
		public const string ELEMENT_GATEWAY_PARALLEL = "parallelGateway";
		public const string ELEMENT_GATEWAY_COMPLEX = "complexGateway";
		public const string ELEMENT_EVENT_START = "startEvent";
		public const string ELEMENT_EVENT_END = "endEvent";
		public const string ELEMENT_EVENT_BOUNDARY = "boundaryEvent";
		public const string ELEMENT_EVENT_THROW = "intermediateThrowEvent";
		public const string ELEMENT_EVENT_CATCH = "intermediateCatchEvent";
		public const string ELEMENT_EVENT_ERRORDEFINITION = "errorEventDefinition";
		public const string ELEMENT_EVENT_MESSAGEDEFINITION = "messageEventDefinition";
		public const string ELEMENT_EVENT_SIGNALDEFINITION = "signalEventDefinition";
		public const string ELEMENT_EVENT_TIMERDEFINITION = "timerEventDefinition";
		public const string ELEMENT_EVENT_TERMINATEDEFINITION = "terminateEventDefinition";
		public const string ELEMENT_EVENT_CANCELDEFINITION = "cancelEventDefinition";
		public const string ELEMENT_EVENT_COMPENSATEDEFINITION = "compensateEventDefinition";
		public const string ELEMENT_FORMPROPERTY = "formProperty";
		public const string ELEMENT_EXTENSIONS_FORMDATA = "formData";
		public const string ELEMENT_EXTENSIONS_FORMFIELD = "formField";
		public const string ELEMENT_EXTENSIONS_PROPERTY = "property";
		public const string ELEMENT_EXTENSIONS_PROPERTIES = "properties";
		public const string ELEMENT_VALUE = "value";
		public const string ELEMENT_FIELD = "field";
		public const string ELEMENT_FIELD_STRING = "string";
		public const string ELEMENT_DI_DIAGRAM = "BPMNDiagram";
		public const string ELEMENT_DI_PLANE = "BPMNPlane";
		public const string ELEMENT_DI_SHAPE = "BPMNShape";
		public const string ELEMENT_DI_EDGE = "BPMNEdge";
		public const string ELEMENT_DI_LABEL = "BPMNLabel";
		public const string ELEMENT_DI_BOUNDS = "Bounds";
		public const string ELEMENT_DI_WAYPOINT = "waypoint";
		public const string ELEMENT_DATA_OBJECT = "dataObject";
		public const string ELEMENT_DATA_VALUE = "value";
		public const string ELEMENT_CUSTOM_RESOURCE = "customResource";
		public const string ELEMENT_RESOURCE_ASSIGNMENT = "resourceAssignmentExpression";
		public const string ELEMENT_FORMAL_EXPRESSION = "formalExpression";
		public const string ELEMENT_RESOURCE_REF = "resourceRef";

		public const string ATTRIBUTE_ORDERING = "ordering";
		public const string ATTRIBUTE_ID = "id";
		public const string ATTRIBUTE_NAME = "name";
		public const string ATTRIBUTE_TYPE = "type";
		public const string ATTRIBUTE_DEFAULT = "default";
		public const string ATTRIBUTE_ITEM_REF = "itemRef";
		public const string ATTRIBUTE_PROCESS_REF = "processRef";
		public const string ATTRIBUTE_PROCESS_EXECUTABLE = "isExecutable";
		public const string ATTRIBUTE_PROCESS_CANDIDATE_USERS = "candidateStarterUsers";
		public const string ATTRIBUTE_PROCESS_CANDIDATE_GROUPS = "candidateStarterGroups";
		public const string ATTRIBUTE_TRIGGERED_BY = "triggeredByEvent";
		public const string ATTRIBUTE_CANCEL_REMAINING_INSTANCES = "cancelRemainingInstances";
		public const string ATTRIBUTE_MESSAGE_EXPRESSION = "messageExpression";
		public const string ATTRIBUTE_SIGNAL_EXPRESSION = "signalExpression";
		public const string ATTRIBUTE_LISTENER_EVENT = "event";
		public const string ATTRIBUTE_LISTENER_EVENTS = "events";
		public const string ATTRIBUTE_LISTENER_ENTITY_TYPE = "entityType";
		public const string ATTRIBUTE_LISTENER_CLASS = "class";
		public const string ATTRIBUTE_LISTENER_EXPRESSION = "expression";
		public const string ATTRIBUTE_LISTENER_DELEGATEEXPRESSION = "delegateExpression";
		public const string ATTRIBUTE_LISTENER_THROW_EVENT_TYPE = "throwEvent";
		public const string ATTRIBUTE_LISTENER_THROW_SIGNAL_EVENT_NAME = "signalName";
		public const string ATTRIBUTE_LISTENER_THROW_MESSAGE_EVENT_NAME = "messageName";
		public const string ATTRIBUTE_LISTENER_THROW_ERROR_EVENT_CODE = "errorCode";
		public const string ATTRIBUTE_LISTENER_ON_TRANSACTION = "onTransaction";
		public const string ATTRIBUTE_LISTENER_CUSTOM_PROPERTIES_RESOLVER_CLASS = "customPropertiesResolverClass";
		public const string ATTRIBUTE_LISTENER_CUSTOM_PROPERTIES_RESOLVER_EXPRESSION = "customPropertiesResolverExpression";
		public const string ATTRIBUTE_LISTENER_CUSTOM_PROPERTIES_RESOLVER_DELEGATEEXPRESSION = "customPropertiesResolverDelegateExpression";
		public const string ATTRIBUTE_LISTENER_THROW_EVENT_TYPE_SIGNAL = "signal";
		public const string ATTRIBUTE_LISTENER_THROW_EVENT_TYPE_GLOBAL_SIGNAL = "globalSignal";
		public const string ATTRIBUTE_LISTENER_THROW_EVENT_TYPE_MESSAGE = "message";
		public const string ATTRIBUTE_LISTENER_THROW_EVENT_TYPE_ERROR = "error";
		public const string ATTRIBUTE_VALUE_TRUE = "true";
		public const string ATTRIBUTE_VALUE_FALSE = "false";
		public const string ATTRIBUTE_ACTIVITY_ASYNCHRONOUS = "async";
		public const string ATTRIBUTE_ACTIVITY_EXCLUSIVE = "exclusive";
		public const string ATTRIBUTE_ACTIVITY_ISFORCOMPENSATION = "isForCompensation";
		public const string ATTRIBUTE_IMPORT_TYPE = "importType";
		public const string ATTRIBUTE_LOCATION = "location";
		public const string ATTRIBUTE_EVENT = "event";
		public const string ATTRIBUTE_EVENT_START_VALUE = "start";
		public const string ATTRIBUTE_NAMESPACE = "namespace";
		public const string ATTRIBUTE_IMPLEMENTATION_REF = "implementationRef";
		public const string ATTRIBUTE_STRUCTURE_REF = "structureRef";
		public const string ATTRIBUTE_ITEM_KIND = "itemKind";
		public const string ATTRIBUTE_ITEM_SUBJECT_REF = "itemSubjectRef";
		public const string ATTRIBUTE_DATA_STORE_REF = "dataStoreRef";
		public const string ATTRIBUTE_EVENT_START_INITIATOR = "initiator";
		public const string ATTRIBUTE_EVENT_START_INTERRUPTING = "isInterrupting";
		public const string ATTRIBUTE_FORM_FORMKEY = "formKey";
		public const string ATTRIBUTE_MULTIINSTANCE_SEQUENTIAL = "isSequential";
		public const string ATTRIBUTE_MULTIINSTANCE_COLLECTION = "collection";
		public const string ATTRIBUTE_MULTIINSTANCE_VARIABLE = "elementVariable";
		public const string ATTRIBUTE_MULTIINSTANCE_INDEX_VARIABLE = "elementIndexVariable";
		public const string ATTRIBUTE_TASK_IMPLEMENTATION = "implementation";
		public const string ATTRIBUTE_TASK_OPERATION_REF = "operationRef";
		public const string ATTRIBUTE_TASK_SCRIPT_TEXT = "script";
		public const string ATTRIBUTE_TASK_SCRIPT_FORMAT = "scriptFormat";
		public const string ATTRIBUTE_TASK_SCRIPT_RESULTVARIABLE = "resultVariable";
		public const string ATTRIBUTE_TASK_SCRIPT_AUTO_STORE_VARIABLE = "autoStoreVariables";
		public const string ATTRIBUTE_TASK_SERVICE_CLASS = "class";
		public const string ATTRIBUTE_TASK_SERVICE_EXPRESSION = "expression";
		public const string ATTRIBUTE_TASK_SERVICE_CONNECTOR = "connector";
		public const string ATTRIBUTE_TASK_SERVICE_DELEGATEEXPRESSION = "delegateExpression";
		public const string ATTRIBUTE_TASK_SERVICE_RESULTVARIABLE = "resultVariableName";
		public const string ATTRIBUTE_TASK_SERVICE_EXTENSIONID = "extensionId";
		public const string ATTRIBUTE_TASK_SERVICE_SKIP_EXPRESSION = "skipExpression";
		public const string ATTRIBUTE_TASK_USER_ASSIGNEE = "assignee";
		public const string ATTRIBUTE_TASK_USER_OWNER = "owner";
		public const string ATTRIBUTE_TASK_USER_CANDIDATEUSERS = "candidateUsers";
		public const string ATTRIBUTE_TASK_USER_CANDIDATEGROUPS = "candidateGroups";
		public const string ATTRIBUTE_TASK_USER_DUEDATE = "dueDate";
		public const string ATTRIBUTE_TASK_USER_BUSINESS_CALENDAR_NAME = "businessCalendarName";
		public const string ATTRIBUTE_TASK_USER_CATEGORY = "category";
		public const string ATTRIBUTE_TASK_USER_PRIORITY = "priority";
		public const string ATTRIBUTE_TASK_USER_SKIP_EXPRESSION = "skipExpression";
		public const string ATTRIBUTE_TASK_RULE_VARIABLES_INPUT = "ruleVariablesInput";
		public const string ATTRIBUTE_TASK_RULE_RESULT_VARIABLE = "resultVariable";
		public const string ATTRIBUTE_TASK_RULE_RULES = "rules";
		public const string ATTRIBUTE_TASK_RULE_EXCLUDE = "exclude";
		public const string ATTRIBUTE_TASK_RULE_CLASS = "class";
		public const string ATTRIBUTE_CALL_ACTIVITY_CALLEDELEMENT = "calledElement";
		public const string ATTRIBUTE_CALL_ACTIVITY_BUSINESS_KEY = "businessKey";
		public const string ATTRIBUTE_CALL_ACTIVITY_INHERIT_BUSINESS_KEY = "inheritBusinessKey";
		public const string ATTRIBUTE_CALL_ACTIVITY_INHERITVARIABLES = "inheritVariables";
		public const string ATTRIBUTE_IOPARAMETER_SOURCE = "source";
		public const string ATTRIBUTE_IOPARAMETER_SOURCE_EXPRESSION = "sourceExpression";
		public const string ATTRIBUTE_IOPARAMETER_TARGET = "target";
		public const string ATTRIBUTE_FLOW_SOURCE_REF = "sourceRef";
		public const string ATTRIBUTE_FLOW_TARGET_REF = "targetRef";
		public const string ATTRIBUTE_FLOW_SKIP_EXPRESSION = "skipExpression";
		public const string ATTRIBUTE_TEXTFORMAT = "textFormat";
		public const string ATTRIBUTE_BOUNDARY_ATTACHEDTOREF = "attachedToRef";
		public const string ATTRIBUTE_BOUNDARY_CANCELACTIVITY = "cancelActivity";
		public const string ATTRIBUTE_ERROR_REF = "errorRef";
		public const string ATTRIBUTE_ERROR_CODE = "errorCode";
		public const string ATTRIBUTE_MESSAGE_REF = "messageRef";
		public const string ATTRIBUTE_SIGNAL_REF = "signalRef";
		public const string ATTRIBUTE_SCOPE = "scope";
		public const string ATTRIBUTE_CALENDAR_NAME = "businessCalendarName";
		public const string ATTRIBUTE_TIMER_DATE = "timeDate";
		public const string ATTRIBUTE_TIMER_CYCLE = "timeCycle";
		public const string ATTRIBUTE_END_DATE = "endDate";
		public const string ATTRIBUTE_TIMER_DURATION = "timeDuration";
		public const string ATTRIBUTE_TERMINATE_ALL = "terminateAll";
		public const string ATTRIBUTE_TERMINATE_MULTI_INSTANCE = "terminateMultiInstance";
		public const string ATTRIBUTE_COMPENSATE_ACTIVITYREF = "activityRef";
		public const string ATTRIBUTE_COMPENSATE_WAITFORCOMPLETION = "waitForCompletion";
		public const string ATTRIBUTE_FORM_ID = "id";
		public const string ATTRIBUTE_FORM_NAME = "name";
		public const string ATTRIBUTE_FORM_TYPE = "type";
		public const string ATTRIBUTE_FORM_EXPRESSION = "expression";
		public const string ATTRIBUTE_FORM_VARIABLE = "variable";
		public const string ATTRIBUTE_FORM_READABLE = "readable";
		public const string ATTRIBUTE_FORM_WRITABLE = "writable";
		public const string ATTRIBUTE_FORM_REQUIRED = "required";
		public const string ATTRIBUTE_FORM_DEFAULT = "default";
		public const string ATTRIBUTE_FORM_DATEPATTERN = "datePattern";
		public const string ATTRIBUTE_FIELD_NAME = "name";
		public const string ATTRIBUTE_FIELD_STRING = "stringValue";
		public const string ATTRIBUTE_FIELD_EXPRESSION = "expression";
		public const string ATTRIBUTE_DI_BPMNELEMENT = "bpmnElement";
		public const string ATTRIBUTE_DI_IS_EXPANDED = "isExpanded";
		public const string ATTRIBUTE_DI_WIDTH = "width";
		public const string ATTRIBUTE_DI_HEIGHT = "height";
		public const string ATTRIBUTE_DI_X = "x";
		public const string ATTRIBUTE_DI_Y = "y";
		public const string ATTRIBUTE_DATA_ID = "id";
		public const string ATTRIBUTE_DATA_NAME = "name";
		public const string ATTRIBUTE_DATA_ITEM_REF = "itemSubjectRef";
		public const string ATTRIBUTE_ASSOCIATION_DIRECTION = "associationDirection";
		public const string ALFRESCO_TYPE = "alfrescoScriptType";
		public const string FAILED_JOB_RETRY_TIME_CYCLE = "failedJobRetryTimeCycle";
		public const string MAP_EXCEPTION = "mapException";
		public const string MAP_EXCEPTION_ERRORCODE = "errorCode";
		public const string MAP_EXCEPTION_ANDCHILDREN = "includeChildExceptions";

		public const string RUNTIME_ASSIGNEE_USER_VARIABLE_NAME = "runtimeAssigneeUser";
		public const string ACTIITI_RUNTIME_ASSIGNEE_VARIABLE = "assigneeVariable";
		public const string ACTIITI_RUNTIME_ASSIGNEE = "runtimeAssignee";

		public const string ACTIVITI_COUNTERSIGNUSER_ATTRIBUTE = "countersignUsers";
		public const string ACTIVITI_COUNTERSIGNUSER_GETPOLICY = "userPolicy";

		public const string CAMUNDA_NAMESPACE_PREFIX = "camunda";

		public static readonly XNamespace BPMN2_XNAMESPACE = XNamespace.Get(BPMN2_NAMESPACE);
		public static readonly XNamespace CAMUNDA_XNAMESPACE = XNamespace.Get(ACTIVITI_EXTENSIONS_NAMESPACE);
		public static readonly XNamespace XSI_XNAMESPACE = XNamespace.Get(XSI_NAMESPACE);
	}
}