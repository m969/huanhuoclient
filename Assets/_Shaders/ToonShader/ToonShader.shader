Shader "Custom/ToonShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white"{}
		_Color("Main Color", Color) = (1,1,1,1)
		_Outline("Outline", range(0,0.2))=0.02
		_Outline2("Outline2", range(0,0.2))=0.02
		_Factor("Factor", Range(0, 1))=0.5
		_Factor2("Factor2", Range(0, 1))=0.5
		_ToonEffect("Toon Effect", Range(0,1)) = 0.5
		_Steps("Steps of toon", Range(0, 9)) = 3
	}

	SubShader
	{
		Pass//描边Pass
		{
			Tags{"LightMode"="Always"}
			Cull Front Zwrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			float _Outline;
			float _Factor;
			struct v2f
			{
				float4 pos:SV_POSITION;
			};
			v2f vert (appdata_full v)
			{
				v2f o;
				o.pos=mul(UNITY_MATRIX_MVP, v.vertex);
				float3 dir = normalize(v.vertex.xyz);
				float3 dir2 = v.normal;
				float D = dot(dir, dir2);
				D = (D/_Factor+1)/(1+1/_Factor);
				dir = lerp(dir, dir2, _Factor);//根据点积的结果在 Normal和Vertex之间进行调节
				dir = mul((float3x3)UNITY_MATRIX_IT_MV, dir);
				float2 offset = TransformViewToProjection(dir.xy);
				offset = normalize(offset);
				o.pos.xy += offset * o.pos.z * _Outline;
				return o;
			}
			float4 frag(v2f i):Color
			{
				float4 c=0;
				return c;
			}
			ENDCG
		}
		
		Pass//棱边Pass
		{
			Tags{"LightMode"="Always"}
			Cull Back ZWrite Off
			CGPROGRAM
			#pragma vertex vert 
			#pragma fragment frag
			#include "UnityCG.cginc"
			float _Outline2;
			float _Factor2;
			struct v2f
			{
				float4 pos:SV_POSITION;
			};
			v2f vert(appdata_full v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				float3 dir = normalize(v.vertex.xyz);
				float3 dir2 = v.normal;
				float D = dot(dir, dir2);
				D = (D/_Factor2+1)/(1+1/_Factor2);
				dir = lerp(dir, dir2, D);
				dir = mul((float3x3)UNITY_MATRIX_IT_MV, dir);
				float2 offset = TransformViewToProjection(dir.xy);
				offset = normalize(offset);
				o.pos.xy += offset * o.pos.z * _Outline2;
				return o;
			}
			float4 frag(v2f i):Color
			{
				return float4(1, 1, 1, 1);
			}
			ENDCG
		}

		Pass//光照着色Pass 
		{
			Tags{"LightMode"="ForwardBase"}
			Cull Back
			//Blend DstColor Zero//利 用 刚刚两个描边Pass 的渲染结果的混合模式
			Blend DstColor Zero
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			float4 _LightColor0;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Factor2;
			float _Outline2;
			float4 _Color;
			float _Steps;
			float _ToonEffect;
			struct v2f
			{
				float4 pos:SV_POSITION;
				float4 uv:TEXCOORD0;
				float3 lightDir:TEXCOORD1;
				float3 viewDir:TEXCOORD2;
				float3 normal:TEXCOORD3;
			};
			v2f vert (appdata_full v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				//为了能让Z检测正常进行，需要进行和Cull Back的Pass同样的顶点挤出操作
				float3 dir = normalize(v.vertex.xyz);
				float3 dir2 = v.normal;
				float D = dot(dir, dir2);
				D = (D/_Factor2+1)/(1+1/D);
				dir = lerp(dir, dir2, D);
				dir = mul((float3x3)UNITY_MATRIX_IT_MV, dir);
				float2 offset = TransformViewToProjection(dir.xy);
				offset = normalize(offset);
				o.pos.xy += offset * o.pos.z * _Outline2;
				o.normal = v.normal;
				o.lightDir = ObjSpaceLightDir(v.vertex);
				o.viewDir = ObjSpaceViewDir(v.vertex);

				o.uv = v.texcoord;
				return o;
			}
			float4 frag(v2f i):Color
			{
				float4 c = 1;
				float3 N = normalize(i.normal);
				float3 viewDir = normalize(i.viewDir);
				float3 lightDir = normalize(i.lightDir);//在ForwardBase中计算一个平行光
				float diff = max(0, dot(N, i.lightDir));//计算漫反射
				diff = (diff+1)/2;
				diff = smoothstep(0, 1, diff);
				float toon = floor(diff * _Steps) / _Steps;//对计算结果值域记性离散化处理
				diff = lerp(diff, toon, _ToonEffect);//再调节卡通效果的强弱
				float4 co = tex2D(_MainTex, i.uv) * _Color;
				c = co * _LightColor0 * diff;
				return c; 
			}
			ENDCG
		}

		Pass
		{
			Tags{"LightMode" = "ForwardAdd"}
			Blend One One
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag 
			#include "UnityCG.cginc"
			float4 _LightColor0;
			float4 _Color;
			float _Steps;
			float _ToonEffect;
			struct v2f
			{
				float4 pos:SV_POSITION;
				float3 lightDir:TEXCOORD0;
				float3 viewDir:TEXCOORD1;
				float3 normal:TEXCOORD2;
			};
			v2f vert(appdata_full v){
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.normal = v.normal;
				o.viewDir = ObjSpaceViewDir(v.vertex);
				o.lightDir = _WorldSpaceLightPos0 - v.vertex;
				return o;
			}
			float4 frag (v2f i):Color
			{
				float4 c = 1;
				float3 N = normalize(i.normal);
				float3 viewDir = normalize(i.viewDir);
				float dist = length(i.lightDir);//计算到光源的距离
				float3 lightDir = normalize(i.lightDir);
				float diff = max(0, dot(N, i.lightDir));//计算漫反射
				diff = (diff + 1) / 2;
				diff = smoothstep(0, 1, diff);
				float atten = 1 / (dist);
				//diff = diff * atten;
				float toon = floor(diff * atten * _Steps);//离散化处理
				diff = lerp(diff, toon, _ToonEffect);//调节卡通效果的强弱
				half3 h = normalize(lightDir + viewDir);//计算高光
				float nh = max(0, dot(N, h));
				float spec = pow(nh, 32.0);
				float toonSpec = floor(spec*atten*2) / 2;//对高光进行二元离散化处理
				spec = lerp(spec, toonSpec, _ToonEffect);
				atten = 1 / (dist);//计算点光源的衰减
				c = _Color * _LightColor0 * (diff + spec) * atten;
				return c;
			}
			ENDCG
		}

		//Pass//照明Pass
		//{
		//	Tags{"LightMode"="ForwardBase"}
		//	CGPROGRAM
		//	#pragma vertex vert
		//	#pragma fragment frag
		//	#include "UnityCG.cginc"
		//	float4 _LightColor0;
		//	struct v2f
		//	{
		//		float4 pos:SV_POSITION;
		//		float3 lightDir:TEXCOORD0;
		//		float3 viewDir:TEXCOORD1;
		//		float3 normal:TEXCOORD2;
		//	};
		//	v2f vert (appdata_full v)
		//	{
		//		v2f o;
		//		o.pos=mul(UNITY_MATRIX_MVP, v.vertex);
		//		o.normal=v.normal;
		//		o.lightDir=ObjSpaceLightDir(v.vertex);
		//		o.viewDir=ObjSpaceViewDir(v.vertex);
		//		return o;
		//	}
		//	float4 frag(v2f i):Color
		//	{
		//		float4 c=1;
		//		float3 N=normalize(i.normal);
		//		float3 viewDir=normalize(i.viewDir);
		//		float diff=dot(N,i.lightDir);
		//		diff=(diff+1)/2;
		//		diff=smoothstep(diff/12,1,diff);
		//		c=_LightColor0*diff;
		//		return c;
		//	}
		//	ENDCG
		//}
	}
}
