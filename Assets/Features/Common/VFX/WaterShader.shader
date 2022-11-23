// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "WaterShader"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Size("Size", Float) = 1
		_SizeNoise("SizeNoise", Float) = 1
		_SizeNoise2("SizeNoise2", Float) = 1
		_DisplaceForce("DisplaceForce", Float) = 0
		_dir("dir", Vector) = (0,0,0,0)
		_dir2("dir2", Vector) = (0,0,0,0)
		_asdas("asdas", Float) = 0
		_powww("powww", Float) = 0
		_Vector0("Vector 0", Vector) = (0,0,0,0)
		_a("a", Color) = (0,0,0,0)
		_b("b", Color) = (0,0,0,0)
		_Float0("Float 0", Float) = 0
		_Float1("Float 1", Float) = 0

	}

	SubShader
	{
		LOD 0

		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }

		Cull Off
		Lighting Off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		
		
		Pass
		{
		CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"
			#define ASE_NEEDS_FRAG_COLOR


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
				float4 ase_texcoord1 : TEXCOORD1;
			};
			
			uniform fixed4 _Color;
			uniform float _EnableExternalAlpha;
			uniform sampler2D _MainTex;
			uniform sampler2D _AlphaTex;
			uniform float4 _b;
			uniform sampler2D _TextureSample0;
			SamplerState sampler_TextureSample0;
			uniform float4 _Vector0;
			uniform float _Size;
			uniform float _SizeNoise;
			uniform float4 _dir;
			uniform float _DisplaceForce;
			uniform float _Float0;
			uniform float4 _a;
			uniform float _Float1;
			uniform float _SizeNoise2;
			uniform float4 _dir2;
			uniform float _powww;
			uniform float _asdas;
			float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }
			float snoise( float2 v )
			{
				const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
				float2 i = floor( v + dot( v, C.yy ) );
				float2 x0 = v - i + dot( i, C.xx );
				float2 i1;
				i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
				float4 x12 = x0.xyxy + C.xxzz;
				x12.xy -= i1;
				i = mod2D289( i );
				float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
				float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
				m = m * m;
				m = m * m;
				float3 x = 2.0 * frac( p * C.www ) - 1.0;
				float3 h = abs( x ) - 0.5;
				float3 ox = floor( x + 0.5 );
				float3 a0 = x - ox;
				m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
				float3 g;
				g.x = a0.x * x0.x + h.x * x0.y;
				g.yz = a0.yz * x12.xz + h.yz * x12.yw;
				return 130.0 * dot( m, g );
			}
			

			
			v2f vert( appdata_t IN  )
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				UNITY_TRANSFER_INSTANCE_ID(IN, OUT);
				float3 ase_worldPos = mul(unity_ObjectToWorld, IN.vertex).xyz;
				OUT.ase_texcoord1.xyz = ase_worldPos;
				
				
				//setting value to unused interpolator channels and avoid initialization warnings
				OUT.ase_texcoord1.w = 0;
				
				IN.vertex.xyz +=  float3(0,0,0) ; 
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
				float4 appendResult24 = (float4(IN.color.r , IN.color.g , IN.color.b , 1.0));
				float3 temp_output_101_0 = (IN.color).rgb;
				float mulTime45 = _Time.y * _Vector0.w;
				float3 ase_worldPos = IN.ase_texcoord1.xyz;
				float2 appendResult4 = (float2(ase_worldPos.x , ase_worldPos.y));
				float2 appendResult19 = (float2(ase_worldPos.x , ase_worldPos.y));
				float mulTime9 = _Time.y * _dir.z;
				float simplePerlin2D119 = snoise( ( ( appendResult19 * _SizeNoise ) + ( mulTime9 * (_dir).xy ) ) );
				simplePerlin2D119 = simplePerlin2D119*0.5 + 0.5;
				float2 appendResult14 = (float2(simplePerlin2D119 , 0.0));
				float4 tex2DNode1 = tex2D( _TextureSample0, ( ( ( (_Vector0).xy * _Vector0.z * mulTime45 ) + ( appendResult4 * _Size ) ) + ( appendResult14 * _DisplaceForce ) ) );
				float TextureWaterG93 = tex2DNode1.g;
				float3 lerpResult102 = lerp( temp_output_101_0 , ( temp_output_101_0 + (_b).rgb ) , ( _b.a * TextureWaterG93 * _Float0 ));
				float TextureWaterR84 = tex2DNode1.r;
				float3 lerpResult103 = lerp( lerpResult102 , ( temp_output_101_0 * (_a).rgb ) , ( TextureWaterR84 * _a.a * _Float1 ));
				float4 appendResult25 = (float4((lerpResult103).xyz , 1.0));
				float4 lerpResult23 = lerp( appendResult24 , appendResult25 , ( 1.0 - ( IN.color.a * 1.0 ) ));
				float2 appendResult29 = (float2(ase_worldPos.x , ase_worldPos.y));
				float2 temp_output_33_0 = ( appendResult29 * _SizeNoise2 );
				float mulTime31 = _Time.y * _dir2.z;
				float2 temp_output_34_0 = ( mulTime31 * (_dir2).xy );
				float simplePerlin2D62 = snoise( ( temp_output_33_0 + temp_output_34_0 ) );
				simplePerlin2D62 = simplePerlin2D62*0.5 + 0.5;
				float simplePerlin2D124 = snoise( ( temp_output_33_0 + ( temp_output_34_0 * float2( 0.5,0.5 ) ) ) );
				simplePerlin2D124 = simplePerlin2D124*0.5 + 0.5;
				float clampResult61 = clamp( ( pow( ( simplePerlin2D62 * simplePerlin2D124 ) , _powww ) + _asdas ) , 0.0 , 1.0 );
				float4 lerpResult26 = lerp( appendResult24 , lerpResult23 , clampResult61);
				
				fixed4 c = lerpResult26;
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
78;12;1360;1007;2482.881;-951.2167;1;True;True
Node;AmplifyShaderEditor.Vector4Node;11;-6884.968,886.3159;Inherit;False;Property;_dir;dir;5;0;Create;True;0;0;False;0;False;0,0,0,0;1,0,0.3,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldPosInputsNode;18;-6928.512,506.8699;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ComponentMaskNode;12;-6697.968,885.3159;Inherit;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleTimeNode;9;-6695.968,807.3159;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;19;-6747.113,535.5972;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-6760.114,647.1972;Inherit;False;Property;_SizeNoise;SizeNoise;2;0;Create;True;0;0;False;0;False;1;0.4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-6585.512,571.4971;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-6499.967,833.3159;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WorldPosInputsNode;3;-6294.074,164.1651;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode;8;-6328.392,684.7603;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector4Node;42;-6109.858,-78.51762;Inherit;False;Property;_Vector0;Vector 0;9;0;Create;True;0;0;False;0;False;0,0,0,0;1,1,0.08,1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;6;-6125.675,304.4924;Inherit;False;Property;_Size;Size;1;0;Create;True;0;0;False;0;False;1;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;119;-6006.672,811.4704;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;4;-6112.674,192.8924;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleTimeNode;45;-5864.934,101.6304;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;43;-5894.857,-62.51763;Inherit;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-5951.073,228.7924;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-5685.857,-33.5176;Inherit;False;3;3;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;14;-5666.664,660.3959;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-5828.631,528.1491;Inherit;False;Property;_DisplaceForce;DisplaceForce;4;0;Create;True;0;0;False;0;False;0;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;46;-5446.934,86.63043;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-5461.631,554.149;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;15;-5181.366,307.0128;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1;-4959.161,268.7165;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;False;-1;4745eabf11fd512498f33940a958ca19;e1602e12c58c6f641ada75dfc4a1aab6;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;63;-3765.512,-1222.694;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;93;-4582.3,337.6294;Inherit;True;TextureWaterG;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;65;-3790.114,-907.6003;Inherit;False;Property;_b;b;11;0;Create;True;0;0;False;0;False;0,0,0,0;0.5330188,0.9430512,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;84;-4578.766,124.4436;Inherit;True;TextureWaterR;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;101;-3562.418,-1211.883;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;115;-3980.93,-576.2709;Inherit;False;Property;_Float0;Float 0;12;0;Create;True;0;0;False;0;False;0;0.46;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;98;-4099.35,-793.0208;Inherit;True;93;TextureWaterG;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;71;-3548.109,-886.8694;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;85;-3561.094,-320.8095;Inherit;True;84;TextureWaterR;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;64;-3779.992,-387.9016;Inherit;False;Property;_a;a;10;0;Create;True;0;0;False;0;False;0,0,0,0;0.1368101,0,0.8962264,0.5372549;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;117;-3557,-63.38329;Inherit;False;Property;_Float1;Float 1;13;0;Create;True;0;0;False;0;False;0;0.64;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;27;-2447.554,1544.913;Inherit;False;Property;_dir2;dir2;6;0;Create;True;0;0;False;0;False;0,0,0,0;1,1,0.3,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;99;-3559.821,-397.8972;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleTimeNode;31;-2258.555,1465.913;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;32;-2260.555,1543.913;Inherit;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;105;-3355.422,-311.1242;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;106;-3231.757,-1028.218;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;104;-3517.065,-768.5842;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;28;-2361.339,1108.995;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LerpOp;102;-2918.452,-1144.718;Inherit;True;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;108;-3129.952,-628.0621;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WireNode;113;-2824.088,-422.6653;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-2048.555,1477.913;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-2192.94,1249.322;Inherit;False;Property;_SizeNoise2;SizeNoise2;3;0;Create;True;0;0;False;0;False;1;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;29;-2179.939,1137.722;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;129;-1906.881,1655.217;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;103;-2569.773,-668.015;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;-2018.339,1173.622;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;111;-918.6085,-569.787;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;128;-1718.881,1587.217;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;35;-1678.46,1167.52;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;110;-831.8373,-533.9319;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;62;-1511.957,1344.276;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;124;-1452.394,1675.905;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;112;-630.9783,-95.34769;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.VertexColorNode;22;150.3571,-534.4472;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;127;-1287.236,1244.389;Inherit;False;Property;_powww;powww;8;0;Create;True;0;0;False;0;False;0;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;-1191.252,1459.218;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;1.2;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;56;101.2438,-26.43553;Inherit;False;True;True;True;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.PowerNode;37;-1052.539,1235.057;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;60;378.8073,-177.933;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-933.8916,1463.512;Inherit;False;Property;_asdas;asdas;7;0;Create;True;0;0;False;0;False;0;0.01;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;38;598.843,448.3171;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;24;326.3571,-516.4471;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;1;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;25;373.2123,3.680906;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;1;False;1;FLOAT4;0
Node;AmplifyShaderEditor.OneMinusNode;109;655.209,-249.4664;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;23;974.4596,-301.4091;Inherit;False;3;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ClampOpNode;61;1142.041,62.62853;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;26;1427.61,-508.7231;Inherit;False;3;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;1786.575,-437.2762;Float;False;True;-1;2;ASEMaterialInspector;0;8;WaterShader;0f8ba0101102bb14ebf021ddadce9b49;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;False;False;False;False;False;False;True;2;False;-1;False;False;False;False;False;True;2;False;-1;False;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;12;0;11;0
WireConnection;9;0;11;3
WireConnection;19;0;18;1
WireConnection;19;1;18;2
WireConnection;21;0;19;0
WireConnection;21;1;20;0
WireConnection;13;0;9;0
WireConnection;13;1;12;0
WireConnection;8;0;21;0
WireConnection;8;1;13;0
WireConnection;119;0;8;0
WireConnection;4;0;3;1
WireConnection;4;1;3;2
WireConnection;45;0;42;4
WireConnection;43;0;42;0
WireConnection;5;0;4;0
WireConnection;5;1;6;0
WireConnection;44;0;43;0
WireConnection;44;1;42;3
WireConnection;44;2;45;0
WireConnection;14;0;119;0
WireConnection;46;0;44;0
WireConnection;46;1;5;0
WireConnection;16;0;14;0
WireConnection;16;1;17;0
WireConnection;15;0;46;0
WireConnection;15;1;16;0
WireConnection;1;1;15;0
WireConnection;93;0;1;2
WireConnection;84;0;1;1
WireConnection;101;0;63;0
WireConnection;71;0;65;0
WireConnection;99;0;64;0
WireConnection;31;0;27;3
WireConnection;32;0;27;0
WireConnection;105;0;85;0
WireConnection;105;1;64;4
WireConnection;105;2;117;0
WireConnection;106;0;101;0
WireConnection;106;1;71;0
WireConnection;104;0;65;4
WireConnection;104;1;98;0
WireConnection;104;2;115;0
WireConnection;102;0;101;0
WireConnection;102;1;106;0
WireConnection;102;2;104;0
WireConnection;108;0;101;0
WireConnection;108;1;99;0
WireConnection;113;0;105;0
WireConnection;34;0;31;0
WireConnection;34;1;32;0
WireConnection;29;0;28;1
WireConnection;29;1;28;2
WireConnection;129;0;34;0
WireConnection;103;0;102;0
WireConnection;103;1;108;0
WireConnection;103;2;113;0
WireConnection;33;0;29;0
WireConnection;33;1;30;0
WireConnection;111;0;103;0
WireConnection;128;0;33;0
WireConnection;128;1;129;0
WireConnection;35;0;33;0
WireConnection;35;1;34;0
WireConnection;110;0;111;0
WireConnection;62;0;35;0
WireConnection;124;0;128;0
WireConnection;112;0;110;0
WireConnection;40;0;62;0
WireConnection;40;1;124;0
WireConnection;56;0;112;0
WireConnection;37;0;40;0
WireConnection;37;1;127;0
WireConnection;60;0;22;4
WireConnection;38;0;37;0
WireConnection;38;1;39;0
WireConnection;24;0;22;1
WireConnection;24;1;22;2
WireConnection;24;2;22;3
WireConnection;25;0;56;0
WireConnection;109;0;60;0
WireConnection;23;0;24;0
WireConnection;23;1;25;0
WireConnection;23;2;109;0
WireConnection;61;0;38;0
WireConnection;26;0;24;0
WireConnection;26;1;23;0
WireConnection;26;2;61;0
WireConnection;0;0;26;0
ASEEND*/
//CHKSM=A9BA7BC7247A029901871E5BB8A121FD075A78DC