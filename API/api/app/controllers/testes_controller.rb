class TestesController < ApplicationController
  # GET /testes
  # GET /testes.json
  def index
    @mensagem = 'A guerra vai começar'

    render json: @mensagem
  end

  # GET /testes/1
  # GET /testes/1.json
  def show
    #@testis = Teste.find(params[:id])
    @mensagem = 'Em construção'

    render json: @mensagem
  end

  # POST /testes
  # POST /testes.json
  def create
    @testis = Teste.new(params[:testis])

    if @testis.save
      render json: @testis, status: :created, location: @testis
    else
      render json: @testis.errors, status: :unprocessable_entity
    end
  end

  # PATCH/PUT /testes/1
  # PATCH/PUT /testes/1.json
  def update
    @testis = Teste.find(params[:id])

    if @testis.update(params[:testis])
      head :no_content
    else
      render json: @testis.errors, status: :unprocessable_entity
    end
  end

  # DELETE /testes/1
  # DELETE /testes/1.json
  def destroy
    @testis = Teste.find(params[:id])
    @testis.destroy

    head :no_content
  end
end
