Shader "War/TroopShader" {
	Properties {
		_Color ("Aditive Color", Color) = (0,0,0,0)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert

		sampler2D _MainTex;

		struct Input {
			float4 color;
		};
		
		void vert (inout appdata_full v, out Input o) {
            UNITY_INITIALIZE_OUTPUT(Input,o);
            o.color = v.color;
        }
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = IN.color + _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
