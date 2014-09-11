Shader "War/Territory"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_OutlineColor("OutlineColor", Color) = (0,0,0,0)
		_OutlineSize("OutLine", Range(0,0.024)) = 0.007
		_Size("Size", Float) = 0.95 
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Blend One OneMinusSrcAlpha
		
		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DUMMY PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
			};
			
			fixed4 _Color;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * (_Color);
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = IN.color;
				c.rgb *= tex2D(_MainTex, IN.texcoord).a;
				return c;
			}
		ENDCG
		}
		
		Pass
		{
		Cull Back
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DUMMY PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				float4 normal   : NORMAL;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
			};
			
			fixed4 _OutlineColor;
			fixed4 _Color;
			half _OutlineSize; 

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = fixed4(0,0,0,0);
				fixed4 mainColor = fixed4(0,0,0,0);
				fixed bord = 0;
				if(_OutlineSize > 0.0){
					mainColor = (tex2D(_MainTex, IN.texcoord + half2(_OutlineSize,_OutlineSize)).a
					 	  	   + tex2D(_MainTex, IN.texcoord - half2(_OutlineSize,_OutlineSize)).a
							   + tex2D(_MainTex, IN.texcoord + half2(-_OutlineSize,_OutlineSize)).a
							   + tex2D(_MainTex, IN.texcoord + half2(_OutlineSize,-_OutlineSize)).a) * _OutlineColor;
					bord = (tex2D(_MainTex, IN.texcoord + half2(_OutlineSize,_OutlineSize) * 0.1).a
						  * tex2D(_MainTex, IN.texcoord - half2(_OutlineSize,_OutlineSize)* 0.1).a
						  * tex2D(_MainTex, IN.texcoord + half2(-_OutlineSize,_OutlineSize)* 0.1).a
						  * tex2D(_MainTex, IN.texcoord + half2(_OutlineSize,-_OutlineSize)* 0.1).a);
				}
	            fixed inside = tex2D(_MainTex, IN.texcoord).a;
	 
	            if(inside > 0.5 && bord > 0.05){
	            	mainColor = fixed4(0,0,0,0);
	            }
	            
	            return mainColor;
			}
		ENDCG
		}
	}
}
