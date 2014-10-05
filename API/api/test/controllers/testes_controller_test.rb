require 'test_helper'

class TestesControllerTest < ActionController::TestCase
  setup do
    @testis = testes(:one)
  end

  test "should get index" do
    get :index
    assert_response :success
    assert_not_nil assigns(:testes)
  end

  test "should create testis" do
    assert_difference('Teste.count') do
      post :create, testis: {  }
    end

    assert_response 201
  end

  test "should show testis" do
    get :show, id: @testis
    assert_response :success
  end

  test "should update testis" do
    put :update, id: @testis, testis: {  }
    assert_response 204
  end

  test "should destroy testis" do
    assert_difference('Teste.count', -1) do
      delete :destroy, id: @testis
    end

    assert_response 204
  end
end
