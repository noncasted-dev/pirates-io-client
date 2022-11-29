// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "CircleShader"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
		_Color1("Color 0", Color) = (1,0.240566,0.240566,1)
		_Color2("Color 1", Color) = (0.1556604,1,0.4133896,1)
		_Toal1("Toal", Float) = 0

	}

	SubShader
	{
		LOD 0

		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		
		
		Pass
		{
		CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnityCG.cginc"
			#define ASE_NEEDS_VERT_POSITION


			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				
			};
			
			uniform fixed4 _Color;
			uniform float _EnableExternalAlpha;
			uniform sampler2D _MainTex;
			uniform sampler2D _AlphaTex;
			uniform float _Toal1;
			uniform float4 _Color1;
			uniform float4 _Color2;

			
			v2f vert( appdata_t IN  )
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				UNITY_TRANSFER_INSTANCE_ID(IN, OUT);
				float3 normalizeResult7 = normalize( IN.vertex.xyz );
				float2 texCoord2 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float Ring41 = texCoord2.y;
				float lerpResult8 = lerp( -1.0 , 1.0 , Ring41);
				float3 temp_output_9_0 = ( normalizeResult7 * lerpResult8 );
				float3 objToWorldDir10 = mul( unity_ObjectToWorld, float4( temp_output_9_0, 0 ) ).xyz;
				float3 normalizeResult11 = normalize( objToWorldDir10 );
				float3 OffsetDir12 = normalizeResult11;
				float3 worldToObjDir25 = mul( unity_WorldToObject, float4( ( OffsetDir12 * ( _Toal1 * 0.5 ) ), 0 ) ).xyz;
				float3 ToalOffset32 = worldToObjDir25;
				float3 normalizeResult15 = normalize( IN.vertex.xyz );
				float3 ToZeroToalCompensatior28 = ( ( temp_output_9_0 * -0.125 ) + ( 0.125 * normalizeResult15 ) );
				
				
				IN.vertex.xyz += ( ToalOffset32 + ToZeroToalCompensatior28 ); 
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

#if ETC1_EXTERNAL_ALPHA
				// get the color from an external texture (usecase: Alpha support for ETC1 on android)
				fixed4 alpha = tex2D (_AlphaTex, uv);
				color.a = lerp (color.a, alpha.r, _EnableExternalAlpha);
#endif //ETC1_EXTERNAL_ALPHA

				return color;
			}
			
			fixed4 frag(v2f IN  ) : SV_Target
			{
				float2 texCoord2 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float Ring41 = texCoord2.y;
				float temp_output_26_0 = ( Ring41 * 2.0 );
				float4 lerpResult35 = lerp( _Color1 , _Color2 , saturate( temp_output_26_0 ));
				float4 lerpResult37 = lerp( lerpResult35 , _Color1 , saturate( ( temp_output_26_0 + -1.0 ) ));
				
				fixed4 c = lerpResult37;
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18500
78;12;1360;1007;1083.492;831.608;1.3;True;True
Node;AmplifyShaderEditor.CommentaryNode;1;-4539.254,-724.7869;Inherit;False;1522.439;352;Comment;3;41;38;2;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-4489.254,-674.787;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;41;-3946.815,-665.3944;Float;False;Ring;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-2749.504,384.5419;Float;False;Constant;_Float3;Float 2;5;0;Create;True;0;0;False;0;False;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;4;-2787.437,232.0983;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;5;-2730.537,534.5034;Inherit;False;41;Ring;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-2729.504,459.5418;Float;False;Constant;_Float4;Float 3;5;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;7;-2610.494,232.6624;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;8;-2524.221,387.213;Inherit;False;3;0;FLOAT;-1;False;1;FLOAT;1;False;2;FLOAT;-1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-2340.781,300.2192;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TransformDirectionNode;10;-2152.308,350.8358;Inherit;False;Object;World;False;Fast;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.NormalizeNode;11;-1921.37,359.1007;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;12;-1647.998,348.093;Float;False;OffsetDir;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-1214.238,593.6837;Float;False;Property;_Toal1;Toal;3;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;14;-2501.232,913.464;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NormalizeNode;15;-2324.289,914.0289;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-2310.639,834.595;Float;False;Constant;_Float5;Float 4;5;0;Create;True;0;0;False;0;False;0.125;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-2394.872,686.2676;Float;False;Constant;_Float1;Float 0;5;0;Create;True;0;0;False;0;False;-0.125;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-1061.554,669.8271;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;19;-1238.603,489.5829;Inherit;False;12;OffsetDir;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-2178.873,630.2676;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;22;-2056.344,-629.5127;Inherit;False;41;Ring;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-2113.639,851.595;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-1039.521,547.7852;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;24;-1900.637,744.5945;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TransformDirectionNode;25;-799.3952,537.202;Inherit;False;World;Object;False;Fast;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-1714.992,-625.8684;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;32;-507.1953,537.5787;Float;False;ToalOffset;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;27;-1575.052,-1069.124;Float;False;Property;_Color1;Color 0;0;0;Create;True;0;0;False;0;False;1,0.240566,0.240566,1;1,0.240566,0.240566,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;28;-1656.464,617.2683;Float;False;ToZeroToalCompensatior;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;29;-1575.052,-893.1241;Float;False;Property;_Color2;Color 1;1;0;Create;True;0;0;False;0;False;0.1556604,1,0.4133896,1;0.1556604,1,0.4133896,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;30;-1297.649,-801.0391;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;31;-1411.23,-468.6461;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;-1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;33;-708.2651,76.06718;Inherit;False;32;ToalOffset;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SaturateNode;34;-1045.368,-616.6096;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;35;-956.994,-933.6538;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;36;-798.6152,166.6269;Inherit;False;28;ToZeroToalCompensatior;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;39;-1071.246,-392.039;Float;False;Property;_Color3;Color 2;2;0;Create;True;0;0;False;0;False;1,0.7976559,0.2216981,1;0.3647059,0.6980392,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;40;-383.9222,54.00198;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;37;-612.5202,-690.8646;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;38;-3907.115,-581.8564;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;1;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;8;CircleShader;0f8ba0101102bb14ebf021ddadce9b49;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;3;1;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;False;False;False;False;False;False;True;2;False;-1;False;False;False;False;False;True;2;False;-1;False;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;41;0;2;2
WireConnection;7;0;4;0
WireConnection;8;0;3;0
WireConnection;8;1;6;0
WireConnection;8;2;5;0
WireConnection;9;0;7;0
WireConnection;9;1;8;0
WireConnection;10;0;9;0
WireConnection;11;0;10;0
WireConnection;12;0;11;0
WireConnection;15;0;14;0
WireConnection;18;0;13;0
WireConnection;23;0;9;0
WireConnection;23;1;17;0
WireConnection;20;0;16;0
WireConnection;20;1;15;0
WireConnection;21;0;19;0
WireConnection;21;1;18;0
WireConnection;24;0;23;0
WireConnection;24;1;20;0
WireConnection;25;0;21;0
WireConnection;26;0;22;0
WireConnection;32;0;25;0
WireConnection;28;0;24;0
WireConnection;30;0;26;0
WireConnection;31;0;26;0
WireConnection;34;0;31;0
WireConnection;35;0;27;0
WireConnection;35;1;29;0
WireConnection;35;2;30;0
WireConnection;40;0;33;0
WireConnection;40;1;36;0
WireConnection;37;0;35;0
WireConnection;37;1;27;0
WireConnection;37;2;34;0
WireConnection;38;0;2;2
WireConnection;38;1;2;2
WireConnection;38;2;2;2
WireConnection;0;0;37;0
WireConnection;0;1;40;0
ASEEND*/
//CHKSM=412E900BBD1AA2BFF72FBD2130525DCA9D8085A3