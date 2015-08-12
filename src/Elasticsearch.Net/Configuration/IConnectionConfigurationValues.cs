﻿using System;
using System.Collections.Specialized;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Serialization;
using Elasticsearch.Net.Connection.Security;

namespace Elasticsearch.Net.Connection
{
	public interface IConnectionConfigurationValues
	{
		/// <summary> The connection pool to use when talking with elasticsearch </summary>
		IConnectionPool ConnectionPool { get; }
		
		/// <summary> The connection implementation to use when talking with elasticsearch </summary>
		IConnection Connection { get; }
		
		/// <summary>The serializer to use to serialize requests and deserialize responses</summary>
		IElasticsearchSerializer Serializer { get; }

		/// <summary>
		/// When set to a value > 0, this will signal the IConnection what the maximum 
		/// concurrent connections is, NEST favors IOCP ports over threads but in multi tenant 
		/// situations this may still proof a valuable throttle
		/// </summary>
		int MaximumAsyncConnections { get; }
		
		/// <summary>
		/// The timeout in milliseconds for each request to Elasticsearch
		/// </summary>
		TimeSpan Timeout { get; }

		/// <summary>
		/// The timeout in milliseconds to use for ping requests, which are issued to determine whether a node is alive
		/// </summary>
		TimeSpan? PingTimeout { get; }

		/// <summary>
		/// The connect timeout in milliseconds
		/// </summary>
		TimeSpan? ConnectTimeout { get; }
		
		/// <summary>
		/// The time to put dead nodes out of rotation (this will be multiplied by the number of times they've been dead)
		/// </summary>
		TimeSpan? DeadTimeout { get; }
		
		/// <summary>
		/// The maximum ammount of time a node is allowed to marked dead
		/// </summary>
		TimeSpan? MaxDeadTimeout { get; }

		/// <summary>
		/// Limits the total runtime including retries separately from <see cref="Timeout"/>
		/// <pre>
		/// When not specified defaults to <see cref="Timeout"/> which itself defaults to 60 seconds
		/// </pre>
		/// </summary>
		TimeSpan? MaxRetryTimeout { get; }

		/// <summary>
		/// When a retryable exception occurs or status code is returned this controls the maximum
		/// amount of times we should retry the call to elasticsearch
		/// </summary>
		int? MaxRetries { get; }

		/// <summary>
		/// This signals that we do not want to send initial pings to unknown/previously dead nodes
		/// and just send the call straightaway
		/// </summary>
		bool DisablePings { get; }

		/// <summary>
		/// Enable gzip compressed requests and responses, do note that you need to configure elasticsearch to set this
		/// <see cref="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-http.html"/>
		/// </summary>
		bool EnableHttpCompression { get; }
		
		/// <summary>
		/// When set will force all connections through this proxy
		/// </summary>
		string ProxyAddress { get; }
		string ProxyUsername { get; }
		string ProxyPassword { get; }
		
		/// <summary>
		/// When set connection information is written on the trace output 
		/// </summary>
		bool TraceEnabled { get; }

		/// <summary>
		/// When enabled, the client will gather as many interesting metrics as it can.
		/// </summary>
		bool MetricsEnabled { get; }

		/// <summary>
		/// Forces the client to pretty format the requests send to elasticsearch
		/// defaults to true;
		/// </summary>
		bool UsesPrettyRequests { get; }

		/// <summary>
		/// Forces elasticsearch to send pretty json responses over the wire
		/// </summary>
		bool UsesPrettyResponses { get; }

		/// <summary>
		/// When set to true will disable (de)serializing directly to the request and response stream and return a byte[]
		/// copy of the raw request and response on elasticsearch calls. Defaults to  false
		/// </summary>
		bool DisableDirectStreaming { get; }

		/// <summary>
		/// Disabled proxy detection on the webrequest, in some cases this may speed up the first connection
		/// your appdomain makes, in other cases it will actually increase the time for the first connection.
		/// No silver bullet! use with care!
		/// </summary>
		bool DisableAutomaticProxyDetection { get; }

		/// <summary>
		/// By default the client disabled http pipelining as elasticsearch did not support it until 1.4
		/// If you are using a version of elasticsearch >= 1.4 you can enable this and expect some performance gains
		/// </summary>
		bool HttpPipeliningEnabled { get; }

		/// <summary>
		/// Instead of following a c/go like error checking on response.IsValid always throw an ElasticsearchServerException
		/// on the client when a call resulted in an exception on the elasticsearch server. 
		/// <para>Reasons for such exceptions could be search parser errors, index missing exceptions</para>
		/// </summary>
		bool ThrowOnElasticsearchServerExceptions { get;  }

		/// <summary>
		/// Sniff the cluster state immediatly on startup
		/// </summary>
		bool SniffsOnStartup { get; }

		/// <summary>
		/// Force a new sniff for the cluster state everytime a connection dies
		/// </summary>
		bool SniffsOnConnectionFault { get; }

		/// <summary>
		/// Force a new sniff for the cluster when the cluster state information is older than
		/// the specified timespan
		/// </summary>
		TimeSpan? SniffInformationLifeSpan { get; }

		/// <summary>
		/// Append these query string parameters automatically to every request
		/// </summary>
		NameValueCollection QueryStringParameters { get; }

		/// <summary>
		/// Try to send these headers for every request
		/// </summary>
		NameValueCollection Headers { get; }

		/// <summary>
		/// Connection status handler that will be called everytime the connection receives anything.
		/// </summary>
		Action<IElasticsearchResponse> ConnectionStatusHandler { get; }

		/// <summary>
		/// Basic access authorization credentials to specify with all requests.
		/// </summary>
		BasicAuthorizationCredentials BasicAuthenticationCredentials { get; }
		
		/// <summary>
		/// KeepAliveTime - specifies the timeout, in milliseconds, with no
        /// activity until the first keep-alive packet is sent. 
		/// </summary>
		TimeSpan? KeepAliveTime { get; }

		/// <summary>
		/// KeepAliveInterval - specifies the interval, in milliseconds, between
        /// when successive keep-alive packets are sent if no acknowledgement is
        /// received. 
		/// </summary>
		TimeSpan? KeepAliveInterval { get; }
	}
}