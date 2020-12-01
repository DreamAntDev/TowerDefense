// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "GAP/ParticlesABMobile_Scroll"
{
	Properties
	{
		[HDR]_Color("Color", Color) = (0,0,0,0)
		[NoScaleOffset]_Mask("Mask", 2D) = "white" {}
		[NoScaleOffset]_MainTex("MainTex", 2D) = "white" {}
		_MainTexTiling("MainTexTiling", Vector) = (1,1,0,0)
		_MainTexSpeed("MainTexSpeed", Vector) = (0,0,0,0)
		_DistortionAmount("DistortionAmount", Range( -1 , 1)) = 0.1741219
		[NoScaleOffset]_DistortionTexture("DistortionTexture", 2D) = "white" {}
		_DistortionTiling1("DistortionTiling", Vector) = (1,1,0,0)
		_DistortionSpeed("DistortionSpeed", Vector) = (0,0.1,0,0)
		_DissolveAmount("DissolveAmount", Float) = 2
		[NoScaleOffset]_DissolveTexture("DissolveTexture", 2D) = "white" {}
		_DissolveTiling1("DissolveTiling", Vector) = (1,1,0,0)
		_DissolveSpeed("DissolveSpeed", Vector) = (0,0.1,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  "RenderType"="Transparent" "Queue"="Transparent" }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 2.0
		#pragma surface surf Unlit alpha:fade keepalpha noshadow noambient novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa noforwardadd 
		struct Input
		{
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
		};

		uniform float4 _Color;
		uniform sampler2D _Mask;
		uniform sampler2D _MainTex;
		uniform float2 _MainTexSpeed;
		uniform float2 _MainTexTiling;
		uniform sampler2D _DistortionTexture;
		uniform float2 _DistortionSpeed;
		uniform float2 _DistortionTiling1;
		uniform float _DistortionAmount;
		uniform sampler2D _DissolveTexture;
		uniform float2 _DissolveSpeed;
		uniform float2 _DissolveTiling1;
		uniform float _DissolveAmount;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_Mask5 = i.uv_texcoord;
			float2 uv_TexCoord37 = i.uv_texcoord * ( _MainTexTiling - float2( 1,1 ) );
			float2 panner27 = ( 1.0 * _Time.y * _MainTexSpeed + uv_TexCoord37);
			float2 uv_TexCoord43 = i.uv_texcoord * _DistortionTiling1;
			float2 panner25 = ( 1.0 * _Time.y * _DistortionSpeed + uv_TexCoord43);
			float4 lerpResult17 = lerp( float4( i.uv_texcoord, 0.0 , 0.0 ) , tex2D( _DistortionTexture, panner25 ) , _DistortionAmount);
			float2 uv_TexCoord41 = i.uv_texcoord * _DissolveTiling1;
			float2 panner30 = ( 1.0 * _Time.y * _DissolveSpeed + uv_TexCoord41);
			float4 temp_cast_5 = (_DissolveAmount).xxxx;
			float4 temp_output_9_0 = ( i.vertexColor * ( _Color * ( tex2D( _Mask, uv_Mask5 ) * ( tex2D( _MainTex, ( float4( panner27, 0.0 , 0.0 ) + lerpResult17 ).rg ).a * pow( tex2D( _DissolveTexture, ( lerpResult17 + float4( panner30, 0.0 , 0.0 ) ).rg ) , temp_cast_5 ) ) ) ) );
			o.Emission = temp_output_9_0.rgb;
			o.Alpha = temp_output_9_0.a;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18707
0;73;1568;675;2854.833;288.4435;1.811625;True;False
Node;AmplifyShaderEditor.Vector2Node;42;-2688.023,547.4665;Inherit;False;Property;_DistortionTiling1;DistortionTiling;7;0;Create;True;0;0;False;0;False;1,1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;26;-2424.052,685.0979;Inherit;False;Property;_DistortionSpeed;DistortionSpeed;8;0;Create;True;0;0;False;0;False;0,0.1;-1,0.1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;43;-2457.891,526.0999;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;25;-2154.052,666.0979;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;22;-2157.932,403.4965;Inherit;True;Property;_DistortionTexture;DistortionTexture;6;1;[NoScaleOffset];Create;True;0;0;False;0;False;28c7aad1372ff114b90d330f8a2dd938;2f67131bd8bcd5540badff1422fbbf7b;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.Vector2Node;38;-2004.389,-161.5048;Inherit;False;Property;_MainTexTiling;MainTexTiling;3;0;Create;True;0;0;False;0;False;1,1;-1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;40;-1743.122,845.2859;Inherit;False;Property;_DissolveTiling1;DissolveTiling;11;0;Create;True;0;0;False;0;False;1,1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleSubtractOpNode;39;-1805.189,-156.9048;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;1,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;23;-1913.933,517.4965;Inherit;True;Property;_TextureSample3;Texture Sample 3;9;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;3;-1631.658,380.9719;Inherit;False;Property;_DistortionAmount;DistortionAmount;5;0;Create;True;0;0;False;0;False;0.1741219;0.25;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;41;-1521.457,832.0023;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;29;-1540.13,967.0123;Inherit;False;Property;_DissolveSpeed;DissolveSpeed;12;0;Create;True;0;0;False;0;False;0,0.1;-1,0.1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TexCoordVertexDataNode;18;-2531.064,227.9673;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;19;-1601.442,18.2576;Inherit;False;Property;_MainTexSpeed;MainTexSpeed;4;0;Create;True;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;37;-1633.689,-177.6048;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;30;-1273.13,873.0123;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;17;-1351.442,231.3646;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TexturePropertyNode;12;-1189.017,569.0877;Inherit;True;Property;_DissolveTexture;DissolveTexture;10;1;[NoScaleOffset];Create;True;0;0;False;0;False;28c7aad1372ff114b90d330f8a2dd938;28c7aad1372ff114b90d330f8a2dd938;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.SimpleAddOpNode;44;-1085.895,796.9648;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT2;0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PannerNode;27;-1375.43,4.495213;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-847.0177,908.0876;Inherit;False;Property;_DissolveAmount;DissolveAmount;9;0;Create;True;0;0;False;0;False;2;1.88;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;16;-937.0177,680.0876;Inherit;True;Property;_TextureSample2;Texture Sample 2;6;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;2;-1101.703,-162.8539;Inherit;True;Property;_MainTex;MainTex;2;1;[NoScaleOffset];Create;True;0;0;False;0;False;c936e49026718a642958d0ce5d715cd1;6b7cbb52cac101a43b9678e01d92c003;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.SimpleAddOpNode;28;-1149.429,159.4951;Inherit;False;2;2;0;FLOAT2;0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TexturePropertyNode;6;-411.7605,-132.3863;Inherit;True;Property;_Mask;Mask;1;1;[NoScaleOffset];Create;True;0;0;False;0;False;03e4ee8d0b5f45045bb12e2930ed4058;03e4ee8d0b5f45045bb12e2930ed4058;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.PowerNode;13;-632.0177,689.0876;Inherit;False;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-805.8174,134.0206;Inherit;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;5;-148.7605,-133.3863;Inherit;True;Property;_TextureSample1;Texture Sample 1;2;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-356.4419,548.3646;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;209.2395,4.613708;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;20;210.2395,-251.3863;Inherit;False;Property;_Color;Color;0;1;[HDR];Create;True;0;0;False;0;False;0,0,0,0;5.656854,5.656854,5.656854,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;8;732.7843,-153.6375;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;540.2395,-14.38629;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;934.7041,-35.3247;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.BreakToComponentsNode;10;1128.349,-35.90201;Inherit;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1338.465,-205.9384;Float;False;True;-1;0;ASEMaterialInspector;0;0;Unlit;GAP/ParticlesABMobile_Scroll;False;False;False;False;True;True;True;True;True;True;True;True;False;False;True;False;False;False;False;False;False;Off;2;False;-1;3;False;-1;True;0;False;-1;0;False;-1;False;3;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;4;1;False;-1;1;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;2;RenderType=Transparent;Queue=Transparent;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;43;0;42;0
WireConnection;25;0;43;0
WireConnection;25;2;26;0
WireConnection;39;0;38;0
WireConnection;23;0;22;0
WireConnection;23;1;25;0
WireConnection;41;0;40;0
WireConnection;37;0;39;0
WireConnection;30;0;41;0
WireConnection;30;2;29;0
WireConnection;17;0;18;0
WireConnection;17;1;23;0
WireConnection;17;2;3;0
WireConnection;44;0;17;0
WireConnection;44;1;30;0
WireConnection;27;0;37;0
WireConnection;27;2;19;0
WireConnection;16;0;12;0
WireConnection;16;1;44;0
WireConnection;28;0;27;0
WireConnection;28;1;17;0
WireConnection;13;0;16;0
WireConnection;13;1;14;0
WireConnection;1;0;2;0
WireConnection;1;1;28;0
WireConnection;5;0;6;0
WireConnection;11;0;1;4
WireConnection;11;1;13;0
WireConnection;7;0;5;0
WireConnection;7;1;11;0
WireConnection;21;0;20;0
WireConnection;21;1;7;0
WireConnection;9;0;8;0
WireConnection;9;1;21;0
WireConnection;10;0;9;0
WireConnection;0;2;9;0
WireConnection;0;9;10;3
ASEEND*/
//CHKSM=E1BACDEA37CE17E64BED6A9C027CDDF76F7821BE