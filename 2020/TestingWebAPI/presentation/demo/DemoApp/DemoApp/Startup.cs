using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExtensionNetCore3;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCore2Blockly;

namespace DemoApp
{
    public class Startup
    {
        

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddCLI();
            services.AddBlockly();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseBlockly();
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseRouting();

            app.UseAuthorization();

            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCLI();
            app.UseBlocklyUI(new BlocklyUIOptions()
            {
                HeaderName="Demo weather blockly",
                StartBlocks = startBlocks
            });; 
        }
        private string startBlocks= @"<xml xmlns='https://developers.google.com/blockly/xml'>
  <variables>
    <variable id='`Sr}Uw=$C`3#J@udWV@w'>n</variable>
    <variable id='^?Rs1f|ZO^YIyZwL}V#H'>var_DateTime</variable>
    <variable id='AFT?AdGn;T}VV0Zfo/Sp'>var_WeatherForecast</variable>
  </variables>
  <block type='variables_set' inline='true' x='20' y='20'>
    <field name='VAR' id='`Sr}Uw=$C`3#J@udWV@w'>n</field>
    <value name='VALUE'>
      <block type='math_number'>
        <field name='NUM'>1</field>
      </block>
    </value>
    <next>
      <block type='controls_repeat_ext' inline='true'>
        <value name='TIMES'>
          <block type='math_number'>
            <field name='NUM'>4</field>
          </block>
        </value>
        <statement name='DO'>
          <block type='variables_set' inline='true'>
            <field name='VAR' id='`Sr}Uw=$C`3#J@udWV@w'>n</field>
            <value name='VALUE'>
              <block type='math_arithmetic'>
                <field name='OP'>MULTIPLY</field>
                <value name='A'>
                  <block type='variables_get'>
                    <field name='VAR' id='`Sr}Uw=$C`3#J@udWV@w'>n</field>
                  </block>
                </value>
                <value name='B'>
                  <block type='math_number'>
                    <field name='NUM'>2</field>
                  </block>
                </value>
              </block>
            </value>
            <next>
              <block type='text_print'>
                <value name='TEXT'>
                  <block type='variables_get'>
                    <field name='VAR' id='`Sr}Uw=$C`3#J@udWV@w'>n</field>
                  </block>
                </value>
                <next>
                  <block type='variables_set'>
                    <field name='VAR' id='^?Rs1f|ZO^YIyZwL}V#H'>var_DateTime</field>
                    <value name='VALUE'>
                      <block type='displayCurrentDate'>
                        <field name='dateFormat'>iso</field>
                      </block>
                    </value>
                    <next>
                      <block type='variables_set'>
                        <field name='VAR' id='AFT?AdGn;T}VV0Zfo/Sp'>var_WeatherForecast</field>
                        <value name='VALUE'>
                          <block type='DemoApp_WeatherForecast'>
                            <value name='val_Date'>
                              <block type='variables_get'>
                                <field name='VAR' id='^?Rs1f|ZO^YIyZwL}V#H'>var_DateTime</field>
                              </block>
                            </value>
                            <value name='val_TemperatureC'>
                              <shadow type='math_number'>
                                <field name='NUM'>0</field>
                              </shadow>
                            </value>
                            <value name='val_Summary'>
                              <shadow type='text'>
                                <field name='TEXT'>zxczxcz</field>
                              </shadow>
                              <block type='text_join'>
                                <mutation items='2'></mutation>
                                <value name='ADD0'>
                                  <block type='text'>
                                    <field name='TEXT'>Andrei</field>
                                  </block>
                                </value>
                                <value name='ADD1'>
                                  <block type='variables_get'>
                                    <field name='VAR' id='`Sr}Uw=$C`3#J@udWV@w'>n</field>
                                  </block>
                                </value>
                              </block>
                            </value>
                          </block>
                        </value>
                        <next>
                          <block type='text_print'>
                            <value name='TEXT'>
                              <block type='WeatherForecast_POST'>
                                <value name='val_forecast'>
                                  <shadow type='DemoApp_WeatherForecast'></shadow>
                                  <block type='variables_get'>
                                    <field name='VAR' id='AFT?AdGn;T}VV0Zfo/Sp'>var_WeatherForecast</field>
                                  </block>
                                </value>
                              </block>
                            </value>
                          </block>
                        </next>
                      </block>
                    </next>
                  </block>
                </next>
              </block>
            </next>
          </block>
        </statement>
      </block>
    </next>
  </block>
</xml>";
    }
}
